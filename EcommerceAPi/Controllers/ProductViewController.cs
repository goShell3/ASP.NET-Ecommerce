using EcommerceApplication.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public ActionResult<ProductResponse> GetProduct(int id)
        {
            var product = _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // [HttpPost]
        // public async Task<ActionResult> CreateProduct([FromBody] CreateProductRequest request)
        // {
        //     await _productService.CreateProductAsync(request.name, request.price);
        //     var createdProduct = await _productService.GetProductByNameAsync(request.name);
        //     return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, null);
        // }

        // [HttpPut("{id}/stock")]
        // public async Task<ActionResult> UpdateProductStock(int id, [FromBody] UpdateProductStockRequest request)
        // {
        //     await _productService.UpdateProductStockAsync(id, request.Stock);
        //     return NoContent();
        // }

        [HttpDelete("{productName}")]

        public async Task<ActionResult> DeleteProductAsync(string name)
        {
            await _productService.DeleteProductAsync(name);
            return NoContent();
        }

    }
}

