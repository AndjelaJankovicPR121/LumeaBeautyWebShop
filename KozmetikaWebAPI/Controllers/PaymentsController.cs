using KozmetikaWebAPI.Data;
using KozmetikaWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace KozmetikaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public PaymentsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession(CreateCheckoutSessionRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var userId = int.Parse(userIdClaim);

            if (request.Items == null || request.Items.Count == 0)
            {
                return BadRequest("Cart is empty.");
            }

            var lineItems = new List<SessionLineItemOptions>();
            decimal totalAmount = 0;

            foreach (var item in request.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);

                if (product == null)
                {
                    return BadRequest($"Product with ID {item.ProductId} does not exist.");
                }

                if (item.Quantity <= 0)
                {
                    return BadRequest("Quantity must be greater than 0.");
                }

                if (item.Quantity > product.StockQuantity)
                {
                    return BadRequest($"Not enough stock for product: {product.Name}.");
                }

                totalAmount += product.Price * item.Quantity;

                lineItems.Add(new SessionLineItemOptions
                {
                    Quantity = item.Quantity,
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "eur",
                        UnitAmount = (long)Math.Round(product.Price),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Name,
                            Description = product.Brand
                        }
                    }
                });
            }

            var successUrl = "http://localhost:5173?payment=success";
            var cancelUrl = "http://localhost:5173?payment=cancelled";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",
                LineItems = lineItems,
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
                Metadata = new Dictionary<string, string>
                {
                    { "userId", userId.ToString() }
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            var transaction = new PaymentTransaction
            {
                UserId = userId,
                StripeSessionId = session.Id,
                Amount = totalAmount,
                Currency = "rsd",
                Status = "Pending",
                ItemsJson = JsonSerializer.Serialize(request.Items),
                CreatedAt = DateTime.Now
            };

            _context.PaymentTransactions.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                url = session.Url
            });
        }

        [AllowAnonymous]
        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var endpointSecret = _configuration["Stripe:WebhookSecret"];

            Event stripeEvent;

            try
            {
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    endpointSecret
                );
            }
            catch
            {
                return BadRequest("Invalid Stripe signature.");
            }

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Session;

                if (session == null)
                {
                    return BadRequest();
                }

                var transaction = await _context.PaymentTransactions
                    .FirstOrDefaultAsync(t => t.StripeSessionId == session.Id);

                if (transaction == null)
                {
                    return NotFound();
                }

                if (transaction.Status == "Paid")
                {
                    return Ok();
                }

                var items = JsonSerializer.Deserialize<List<CreateCheckoutSessionItemRequest>>(transaction.ItemsJson);

                if (items == null || items.Count == 0)
                {
                    transaction.Status = "Failed";
                    await _context.SaveChangesAsync();
                    return Ok();
                }

                decimal totalAmount = 0;
                var orderItems = new List<OrderItem>();

                foreach (var item in items)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);

                    if (product == null || item.Quantity > product.StockQuantity)
                    {
                        transaction.Status = "StockFailed";
                        await _context.SaveChangesAsync();
                        return Ok();
                    }

                    product.StockQuantity -= item.Quantity;

                    orderItems.Add(new OrderItem
                    {
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        UnitPrice = product.Price
                    });

                    totalAmount += product.Price * item.Quantity;
                }

                var user = await _context.Users.FindAsync(transaction.UserId);

                var order = new Order
                {
                    UserId = transaction.UserId,
                    OrderDate = DateTime.Now,
                    TotalAmount = totalAmount,
                    Status = "Paid",
                    PaymentStatus = "Paid",
                    ShippingAddress = user?.Address ?? string.Empty,
                    PhoneNumber = user?.PhoneNumber ?? string.Empty,
                    OrderItems = orderItems
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                transaction.Status = "Paid";
                transaction.PaymentIntentId = session.PaymentIntentId;
                transaction.PaidAt = DateTime.Now;
                transaction.OrderId = order.Id;

                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _context.PaymentTransactions
                .Include(t => t.User)
                .Include(t => t.Order)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return Ok(transactions);
        }
    }

    public class CreateCheckoutSessionRequest
    {
        [Required]
        [MinLength(1)]
        public List<CreateCheckoutSessionItemRequest> Items { get; set; } = new();
    }

    public class CreateCheckoutSessionItemRequest
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}