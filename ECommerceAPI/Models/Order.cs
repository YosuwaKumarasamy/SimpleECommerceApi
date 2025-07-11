using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerceAPI.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]   // FK
        public int CustomerId { get; set; }

        [JsonIgnore]    // FK USES ONLY [JsonIgnore]
        public Customer Customer { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal OrderAmount { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
