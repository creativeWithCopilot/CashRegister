using Microsoft.EntityFrameworkCore;
using CashRegister.Data;
using CashRegister.DTOs.CategoryDTOs;
using CashRegister.Models;
using CashRegister.DTOs;

namespace CashRegister.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;
        
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<CategoryResponseDTO>> CreateCategoryAsync(CategoryCreateDTO categoryDto)
        {
            try
            {
                // Check if category name already exists (case-insensitive)
                if (await _context.Categories.AnyAsync(c => c.Name.ToLower() == categoryDto.Name.ToLower()))
                {
                    return new ApiResponse<CategoryResponseDTO>(400, "This category already exists.");
                }

                // Manual mapping from DTO to Model
                var category = new Category
                {
                    Name = categoryDto.Name,
                    Description = categoryDto.Description,
                    IsActive = true,
                };

                // Add category to the database
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                // Map to CategoryResponseDTO
                var categoryResponse = new CategoryResponseDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    IsActive = category.IsActive
                };

                return new ApiResponse<CategoryResponseDTO>(200, categoryResponse);
            }
            catch (Exception ex)
            {
                // Log the exception (implementation depends on your logging setup)
                return new ApiResponse<CategoryResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<CategoryResponseDTO>> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _context.Categories
                    .AsNoTracking()  // A query method instructing EF Core to ignore tracked entities.
                    .FirstOrDefaultAsync(c => c.Id == id); // Returns the first match found, or null if no match is found.

                if (category == null)
                {
                    return new ApiResponse<CategoryResponseDTO>(404, "This category was not found.");
                }

                // Map to CategoryResponseDTO
                var categoryResponse = new CategoryResponseDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    IsActive = category.IsActive
                };

                return new ApiResponse<CategoryResponseDTO>(200, categoryResponse);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ApiResponse<CategoryResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateCategoryAsync(CategoryUpdateDTO categoryDto)
        {
            try
            {
                var category = await _context.Categories.FindAsync(categoryDto.Id); //  Get an entity asynchronously using its primary key.

                if (category == null)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(404, "This category already exists.");
                }

                // Check if the new category name already exists (case-insensitive), excluding the current category
                if (await _context.Categories.AnyAsync(c => c.Name.ToLower() == categoryDto.Name.ToLower() && c.Id != categoryDto.Id)) // Checks if any elements in a sequence satisfy a condition or if the sequence is not empty.
                {
                    return new ApiResponse<ConfirmationResponseDTO>(400, "Another category with the same name already exists.");
                }

                // Update product properties manually
                category.Name = categoryDto.Name;
                category.Description = categoryDto.Description;
                category.IsActive = categoryDto.IsActive;

                await _context.SaveChangesAsync();

                // Prepare confirmation message
                var confirmationMessage = new ConfirmationResponseDTO
                {
                    Message = $"Category with Id {categoryDto.Id} updated successfully."
                };

                return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ApiResponse<ConfirmationResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteCategoryAsync(int id )
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id); // Returns the first match found, or null if no match is found.

                if (category == null)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(404, "This category was not found.");
                }

                //Soft Delete
                category.IsActive = false;
                await _context.SaveChangesAsync();

                // Prepare confirmation message
                var confirmationMessage = new ConfirmationResponseDTO
                {
                    Message = $"Category with Id {id} deleted successfully."
                };

                return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ApiResponse<ConfirmationResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<CategoryResponseDTO>>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _context.Categories
                    .AsNoTracking() // A query method instructing EF Core to ignore tracked entities.
                    .ToListAsync(); // that executes a database query and returns the results as a List<T> without blocking the calling thread

                var categoryList = categories.Select(c => new CategoryResponseDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    IsActive = c.IsActive,
                }).ToList();

                return new ApiResponse<List<CategoryResponseDTO>>(200, categoryList);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ApiResponse<List<CategoryResponseDTO>>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }
    }
}