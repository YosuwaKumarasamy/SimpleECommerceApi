using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECommerceAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Customer Email Is Required")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [StringLength(100)]
        public string Password { get; set; }

        [JsonIgnore]
        public List<Order> Orders { get; set; }
    }
}
