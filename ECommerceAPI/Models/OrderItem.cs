
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ECommerceAPI.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Required] // FK 
        public int OrderId { get; set; }

        [JsonIgnore]  // FK USES ONLY [JsonIgnore]
        public Order order { get; set; }

        [Required]   // FK 
        public int ProductId { get; set; }

        [JsonIgnore]   // FK USES ONLY [JsonIgnore]
        public Product Product { get; set; }

        [Range(1, 1000, ErrorMessage = "Quantity Must be Between 1 and 1000")]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price Cannot Be Negative Number")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
    }
}
