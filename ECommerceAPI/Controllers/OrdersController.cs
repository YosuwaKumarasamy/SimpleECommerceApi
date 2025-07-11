using ECommerceAPI.Data;
using ECommerceAPI.DTOs;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderItem = ECommerceAPI.Models.OrderItem;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ECommerceDbContext _context;

        public OrdersController(ECommerceDbContext context)
        {
            _context = context;
        }

        // Customer => Order => OrderItem 
        [HttpGet("GetOrderbyId/{id}")]
        public async Task<ActionResult<Order>> GetOrderById([FromRoute] int id)
        {
            var order = await _context.Order.Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                        .Include(o => o.Customer)
                        .FirstOrDefaultAsync(o => o.Id == id);
            if(order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        //[HttpPost("CreateOrder")]
        //public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderDTO orderDto)
        //{
        //    using var transaction = await _context.Database.BeginTransactionAsync();

        //    try
        //    {
        //        // Validate customer
        //        var customer = await _context.Customer.FindAsync(orderDto.CustomerId);
        //        if (customer == null)
        //        {
        //            return BadRequest("Customer does not exist.");
        //        }

        //        // Initialize order
        //        var order = new Order
        //        {
        //            CustomerId = orderDto.CustomerId,
        //            OrderDate = DateTime.UtcNow,
        //            OrderStatus = "Processing",
        //            OrderItems = new List<OrderItem>()
        //        };

        //        decimal totalAmount = 0;

        //        foreach (var item in orderDto.Items)
        //        {
        //            var product = await _context.Product.FindAsync(item.ProductId);
        //            if (product == null)
        //            {
        //                return BadRequest($"Product with ID {item.ProductId} does not exist.");
        //            }

        //            if (product.Stock < item.Quantity)
        //            {
        //                return BadRequest($"Insufficient stock for product {product.Name}.");
        //            }

        //            // Deduct stock
        //            product.Stock -= item.Quantity;

        //            // Add item to order
        //            var orderItem = new OrderItem
        //            {
        //                ProductId = item.ProductId,
        //                Quantity = item.Quantity,
        //                UnitPrice = product.Price
        //            };

        //            totalAmount += item.Quantity * product.Price;
        //            order.OrderItems.Add(orderItem);
        //        }

        //        order.OrderAmount = totalAmount;

        //        _context.Order.Add(order);
        //        await _context.SaveChangesAsync();
        //        await transaction.CommitAsync();

        //        // Prepare and return a response DTO
        //        var orderDetailsDto = new OrderDetailsDTO
        //        {
        //            Id = order.Id,
        //            CustomerName = customer.Name,
        //            OrderDate = order.OrderDate,
        //            OrderStatus = order.OrderStatus,
        //            OrderAmount = order.OrderAmount,
        //            Items = order.OrderItems.Select(oi => new OrderItemDTO
        //            {
        //                ProductId = oi.ProductId,
        //                ProductName = _context.Product.First(p => p.Id == oi.ProductId).Name,
        //                Quantity = oi.Quantity,
        //                UnitPrice = oi.UnitPrice
        //            }).ToList()
        //        };

        //        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, orderDetailsDto);
        //    }
        //    catch
        //    {
        //        await transaction.RollbackAsync();
        //        throw;
        //    }
        //}

    }
}
