
namespace ECommerceAPI.Controllers
{
    internal class OrderDetailsDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public decimal OrderAmount { get; set; }
        public object Items { get; set; }
    }
}