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
        
    }
}
