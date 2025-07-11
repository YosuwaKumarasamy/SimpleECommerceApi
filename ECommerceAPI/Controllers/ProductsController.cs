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
    public class ProductsController : ControllerBase
    {
        private readonly ECommerceDbContext _context;

        public ProductsController(ECommerceDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] string? name,
            [FromQuery] string? category, [FromQuery] decimal? minprice, [FromQuery] decimal? maxprice)
        {
            var qry = _context.Product.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                qry = qry.Where(x => x.Name == name);
            }
            if (!string.IsNullOrEmpty(category))
            {
                qry = qry.Where(x => x.Category == category);
            }
            if (minprice.HasValue)
            {
                qry = qry.Where(x => x.Price <= minprice);
            }
            if (maxprice.HasValue)
            {
                qry = qry.Where(x => x.Price >= maxprice);
            }
            var products = await qry.ToListAsync();
            return Ok(products);
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<Product>> GetProductById([FromRoute] int id)
        {
            var data = await _context.Product.FindAsync(id);
            if (data == null)
            {
                return BadRequest("Product Not Found");
            }
            return Ok(data);
        }

        [HttpPost("CreateProduct")]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductCreateDTO productCreateDTO)
        {
            var qry = new Product
            {
                Name = productCreateDTO.Name,
                Description = productCreateDTO.Description,
                Category = productCreateDTO.Category,
                Price = productCreateDTO.Price,
                Stock = productCreateDTO.Stock
            };

            _context.Product.Add(qry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = qry.Id }, qry);
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromQuery] decimal price)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            product.Price = price;
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpGet("paged")]
        public async Task<ActionResult<List<Product>>> GetProductsPaged([FromQuery] int pageNumber = 1, [FromQuery] int PageSize = 5)
        {
            var products = await _context.Product.Skip((pageNumber - 1) * PageSize).Take(PageSize).AsNoTracking().ToListAsync();
            return Ok(products);
        }

        [HttpPost("{id}/upload")]
        public async Task<IActionResult> UploadImage([FromRoute] int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return NotFound();
            }

            var data = await _context.Product.FindAsync(id);
            if (data == null)
                return NotFound();
            var fileName = Path.GetFileName(file.FileName);
            return Ok(new { Message = "Image Uploaded Successfully..!", FileName = fileName });
        }


    }
}
