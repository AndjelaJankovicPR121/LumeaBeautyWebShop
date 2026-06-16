using KozmetikaWebAPI.Data;
using KozmetikaWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KozmetikaWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class UsersController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public UsersController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			var users = await _context.Users
				.Include(u => u.Role)
				.OrderBy(u => u.Id)
				.ToListAsync();

			return Ok(users);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUser(int id)
		{
			var user = await _context.Users
				.Include(u => u.Role)
				.FirstOrDefaultAsync(u => u.Id == id);

			if (user == null)
			{
				return NotFound("User not found.");
			}

			return Ok(user);
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser(User user)
		{
			var roleExists = await _context.Roles
				.AnyAsync(r => r.Id == user.RoleId);

			if (!roleExists)
			{
				return BadRequest("Role does not exist.");
			}

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetUser),
				new { id = user.Id },
				user);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(int id, User user)
		{
			if (id != user.Id)
			{
				return BadRequest("User ID mismatch.");
			}

			var existingUser = await _context.Users.FindAsync(id);

			if (existingUser == null)
			{
				return NotFound("User not found.");
			}

			existingUser.Username = user.Username;
			existingUser.Email = user.Email;
			existingUser.FullName = user.FullName;
			existingUser.Address = user.Address;
			existingUser.PhoneNumber = user.PhoneNumber;
			existingUser.RoleId = user.RoleId;

			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			var user = await _context.Users.FindAsync(id);

			if (user == null)
			{
				return NotFound("User not found.");
			}

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}