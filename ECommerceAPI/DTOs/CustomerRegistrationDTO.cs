using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs
{
    public class CustomerRegistrationDTO
    {
        [Required(ErrorMessage = "Customer name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [StringLength(100)]
        public string Password { get; set; }

    }
}
