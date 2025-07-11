using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name Is Required")]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "Product CateGory Is Required")]
        public string Category { get; set; }

        [Range(0.01, 100000, ErrorMessage = "Price Between 0.01 and 100000")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Range(0,1000,ErrorMessage = "Stock Cannot Be Negative Value")]
        public int Stock { get; set; }
    }
}
