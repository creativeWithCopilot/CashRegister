using Microsoft.AspNetCore.Mvc;
using CashRegister.DTOs;
using CashRegister.DTOs.CategoryDTOs;
using CashRegister.Services;

namespace CashRegister.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        // Injecting the CategoryService
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        // Creates a new type of category.
        [HttpPost("CreateCategory")]
        public async Task<ActionResult<ApiResponse<CategoryResponseDTO>>> CreateCategory([FromBody] CategoryCreateDTO taxDto)
        {
            var response = await _categoryService.CreateCategoryAsync(taxDto);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        // Retrieves a category by ID.
        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult<ApiResponse<CategoryResponseDTO>>> GetCategoryById(int id)
        {
            var response = await _categoryService.GetCategoryByIdAsync(id);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        // Updates an existing category.
        [HttpPut("UpdateCategory")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateCategory([FromBody] CategoryUpdateDTO categoryDto)
        {
            var response = await _categoryService.UpdateCategoryAsync(categoryDto);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        // Deletes a category by ID.
        [HttpDelete("DeleteCategory")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteCategory(CategoryDeleteDTO categoryDeleteDto)
        {
            var response = await _categoryService.DeleteCategoryAsync(categoryDeleteDto);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        // Retrieves all category.
        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<ApiResponse<List<CategoryResponseDTO>>>> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategoriesAsync();
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
    }
}