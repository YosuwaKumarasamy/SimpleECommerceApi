using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs
{
    public class CustomerLoginDTO
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
