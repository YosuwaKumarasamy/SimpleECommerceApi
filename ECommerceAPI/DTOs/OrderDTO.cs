using ECommerceAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs
{
    public class OrderDTO
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public List<OrderItem> Items { get; set; }
    }
}
