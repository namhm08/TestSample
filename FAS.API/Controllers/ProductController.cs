using FAS.BLL.BusinessInterfaces;
using FAS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FAS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        // Define API endpoints here
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productService.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            var result = await productService.CreateProductAsync(product);
            if (result)
            {
                return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating product");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody]Product product)
        {
            if (product == null || product.ProductId != id)
            {
                return BadRequest();
            }
            var existingProduct = await productService.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            var result = await productService.UpdateProductAsync(product);
            if (result)
            {
                return NoContent();
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Error updating product");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var existingProduct = await productService.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            var result = await productService.DeleteProductAsync(id);
            if (result)
            {
                return NoContent();
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting product");
        }
    }
}
