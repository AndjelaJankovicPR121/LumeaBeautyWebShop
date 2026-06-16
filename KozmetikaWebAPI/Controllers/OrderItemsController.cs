using KozmetikaWebAPI.Data;
using KozmetikaWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KozmetikaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class OrderItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderItems()
        {
            var items = await _context.OrderItems
                .Include(i => i.Order)
                .Include(i => i.Product)
                .OrderBy(i => i.Id)
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItem(int id)
        {
            var item = await _context.OrderItems
                .Include(i => i.Order)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                return NotFound("Order item not found.");
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem(OrderItem item)
        {
            var orderExists = await _context.Orders.AnyAsync(o => o.Id == item.OrderId);
            var product = await _context.Products.FindAsync(item.ProductId);

            if (!orderExists)
            {
                return BadRequest("Order does not exist.");
            }

            if (product == null)
            {
                return BadRequest("Product does not exist.");
            }

            if (item.Quantity <= 0)
            {
                return BadRequest("Quantity must be greater than 0.");
            }

            item.UnitPrice = product.Price;

            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderItem), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItem item)
        {
            if (id != item.Id)
            {
                return BadRequest("Order item ID mismatch.");
            }

            var existingItem = await _context.OrderItems.FindAsync(id);

            if (existingItem == null)
            {
                return NotFound("Order item not found.");
            }

            var product = await _context.Products.FindAsync(item.ProductId);

            if (product == null)
            {
                return BadRequest("Product does not exist.");
            }

            if (item.Quantity <= 0)
            {
                return BadRequest("Quantity must be greater than 0.");
            }

            existingItem.OrderId = item.OrderId;
            existingItem.ProductId = item.ProductId;
            existingItem.Quantity = item.Quantity;
            existingItem.UnitPrice = product.Price;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var item = await _context.OrderItems.FindAsync(id);

            if (item == null)
            {
                return NotFound("Order item not found.");
            }

            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}