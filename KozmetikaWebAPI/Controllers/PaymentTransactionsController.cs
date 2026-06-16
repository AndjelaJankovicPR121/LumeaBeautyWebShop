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
    public class PaymentTransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentTransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentTransactions()
        {
            var transactions = await _context.PaymentTransactions
                .Include(t => t.User)
                .Include(t => t.Order)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentTransaction(int id)
        {
            var transaction = await _context.PaymentTransactions
                .Include(t => t.User)
                .Include(t => t.Order)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
            {
                return NotFound("Payment transaction not found.");
            }

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentTransaction(PaymentTransaction transaction)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == transaction.UserId);

            if (!userExists)
            {
                return BadRequest("User does not exist.");
            }

            if (transaction.OrderId.HasValue)
            {
                var orderExists = await _context.Orders.AnyAsync(o => o.Id == transaction.OrderId.Value);

                if (!orderExists)
                {
                    return BadRequest("Order does not exist.");
                }
            }

            _context.PaymentTransactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaymentTransaction), new { id = transaction.Id }, transaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentTransaction(int id, PaymentTransaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest("Payment transaction ID mismatch.");
            }

            var existingTransaction = await _context.PaymentTransactions.FindAsync(id);

            if (existingTransaction == null)
            {
                return NotFound("Payment transaction not found.");
            }

            existingTransaction.UserId = transaction.UserId;
            existingTransaction.OrderId = transaction.OrderId;
            existingTransaction.StripeSessionId = transaction.StripeSessionId;
            existingTransaction.PaymentIntentId = transaction.PaymentIntentId;
            existingTransaction.Amount = transaction.Amount;
            existingTransaction.Currency = transaction.Currency;
            existingTransaction.Status = transaction.Status;
            existingTransaction.ItemsJson = transaction.ItemsJson;
            existingTransaction.PaidAt = transaction.PaidAt;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("{id}/mark-as-paid")]
        public async Task<IActionResult> MarkAsPaid(int id)
        {
            var transaction = await _context.PaymentTransactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound("Payment transaction not found.");
            }

            transaction.Status = "Paid";
            transaction.PaidAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return NoContent();
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentTransaction(int id)
        {
            var transaction = await _context.PaymentTransactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound("Payment transaction not found.");
            }

            _context.PaymentTransactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}