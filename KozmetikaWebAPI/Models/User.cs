using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KozmetikaWebAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [StringLength(150)]
        public string Address { get; set; } = string.Empty;

        [StringLength(30)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public Role? Role { get; set; }

        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        [JsonIgnore]
        public ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();

        [JsonIgnore]
        public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    }
}