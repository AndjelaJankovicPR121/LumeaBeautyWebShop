using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KozmetikaWebAPI.Models
{
    public class PaymentTransaction
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public int? OrderId { get; set; }

        public Order? Order { get; set; }

        [Required]
        [StringLength(200)]
        public string StripeSessionId { get; set; } = string.Empty;

        [StringLength(200)]
        public string? PaymentIntentId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999999.99)]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(10)]
        public string Currency { get; set; } = "rsd";

        [Required]
        [StringLength(30)]
        public string Status { get; set; } = "Pending";

        public string ItemsJson { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? PaidAt { get; set; }
    }
}