using ECommerceAPI.Data;
using ECommerceAPI.DTOs;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ECommerceDbContext _context;

        public CustomersController(ECommerceDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomId(int id)
        {
            var data = await _context.Customer.FindAsync(id);
            if(data == null)
            {
                return BadRequest();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CustomRegister([FromForm] CustomerRegistrationDTO registrationDTO)
        {
            
            if(await _context.Customer.AnyAsync(c => c.Email == registrationDTO.Email))
            {
                return BadRequest("Email Already Exist");
            }

            var data = new Customer
            {
                Email = registrationDTO.Email,
                Name = registrationDTO.Name,
                Password = registrationDTO.Password
            };

            _context.Customer.Add(data);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomId), new { id = data.Id }, data);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromHeader(Name = "X-Client-ID")] string clientId, [FromBody] CustomerLoginDTO customer)
        {
            if (string.IsNullOrEmpty(clientId))
                return BadRequest("Missing X-Client-ID Id");

            var data =await _context.Customer.FirstOrDefaultAsync(x => x.Email == customer.Email && x.Password == customer.Password);

            if(data == null)
            {
                return Unauthorized("Invalid Email Or Password");
            }

            return Ok(new { Message = "Authentication SuccessFull..!" });
        }
    }
}
