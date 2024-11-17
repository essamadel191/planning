using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext context;

        public ProductsController(StoreContext context)
        {
            this.context = context;
        }

        // ActionResults Allow Us to return HTTP responses
        // IEnumerable Type of List of Type Prodcut
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            return await context.Products.ToListAsync();
        }

        // For Specific Product
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            var Product = await context.Products.FindAsync(id);

            // Definsive Check for the product
            if(Product == null) return NotFound();

            return Product;
        }

        [HttpPost]
        public async Task<ActionResult<Products>> CreateProduct(Products product)
        {
            context.Products.Add(product);

            await context.SaveChangesAsync();
            
            return product;
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id,Products product)
        {
            if (product.Id != id || !ProductExists(id)) 
                return BadRequest("Connot Update this product");

            // This tell the entity framework tracker that we're passing in here is 
            // an entity effectivily abd has been modified
            context.Entry(product).State = EntityState.Modified;
            
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null) return NotFound("This Product of id : "+id+" is not found");

            // This mean that dotnet is tracking this product
            context.Products.Remove(product);

            // Then Update the database 
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return context.Products.Any(x => x.Id == id);
        }
    }
}
