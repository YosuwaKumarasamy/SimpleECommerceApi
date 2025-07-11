using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs
{
    public class ProductCreateDTO
    {
        [Required(ErrorMessage = "Product Name is Required")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Product Category Is Required")]
        public string Category { get; set; }

        [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01 and 100000")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be a Negative value")]
        public int Stock { get; set; }
    }
}
