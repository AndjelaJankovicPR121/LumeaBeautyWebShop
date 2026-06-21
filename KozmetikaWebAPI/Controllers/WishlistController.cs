using KozmetikaWebAPI.Data;
using KozmetikaWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KozmetikaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer,Admin")]
    public class WishlistController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WishlistController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetWishlist()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var items = await _context.WishlistItems
                .Where(w => w.UserId == userId)
                .Include(w => w.Product)
                    .ThenInclude(p => p.Category)
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync();

            return Ok(items);
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> AddToWishlist(int productId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var productExists = await _context.Products.AnyAsync(p => p.Id == productId);

            if (!productExists)
            {
                return NotFound("Product not found.");
            }

            var alreadyExists = await _context.WishlistItems
                .AnyAsync(w => w.UserId == userId && w.ProductId == productId);

            if (alreadyExists)
            {
                return BadRequest("Product already exists in wishlist.");
            }

            var item = new WishlistItem
            {
                UserId = userId,
                ProductId = productId,
                CreatedAt = DateTime.Now
            };

            _context.WishlistItems.Add(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveFromWishlist(int productId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var item = await _context.WishlistItems
                .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);

            if (item == null)
            {
                return NotFound("Product not found in wishlist.");
            }

            _context.WishlistItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}