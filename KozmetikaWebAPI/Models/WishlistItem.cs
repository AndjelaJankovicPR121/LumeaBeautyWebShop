using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KozmetikaWebAPI.Models
{
    public class WishlistItem
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}