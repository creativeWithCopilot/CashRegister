using Microsoft.AspNetCore.Mvc;
using CashRegister.DTOs;
using CashRegister.DTOs.ProductDTOs;
using CashRegister.Services;

namespace CashRegister.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        // Injecting the ProductService
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // Creates a new product.
        [HttpPost("CreateProduct")]
        public async Task<ActionResult<ApiResponse<ProductResponseDTO>>> CreateProduct([FromBody] ProductCreateDTO productDto)
        {
            var response = await _productService.CreateProductAsync(productDto);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        // Retrieves a product by ID.
        [HttpGet("GetProductById/{pluCode}")]
        public async Task<ActionResult<ApiResponse<ProductResponseDTO>>> GetProductById(string pluCode)
        {
            var response = await _productService.GetProductByIdAsync(pluCode);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        // Updates an existing product.
        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateProduct([FromBody] ProductUpdateDTO productDto)
        {
            var response = await _productService.UpdateProductAsync(productDto);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        // Deletes a product by ID.
        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteProduct(ProductDeleteDTO productDeleteDTO)
        {
            var response = await _productService.DeleteProductAsync(productDeleteDTO);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        // Retrieves all products.
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<ApiResponse<List<ProductResponseDTO>>>> GetAllProducts()
        {
            var response = await _productService.GetAllProductsAsync();
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        // Retrieves all products by category.
        [HttpGet("GetAllProductsByCategory/{categoryId}")]
        public async Task<ActionResult<ApiResponse<List<ProductResponseDTO>>>> GetAllProductsByCategory(int categoryId)
        {
            var response = await _productService.GetAllProductsByCategoryAsync(categoryId);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
    }
}