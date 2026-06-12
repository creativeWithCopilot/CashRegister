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
                    return new ApiResponse<CategoryResponseDTO>(400, "This category name already exists.");
                }

                // Check if Tax exists
                if (!await _context.Taxes.AnyAsync(t => t.Id == categoryDto.TaxId))
                {
                    return new ApiResponse<CategoryResponseDTO>(400, "The specified type of tax does not exist.");
                }

                // Manual mapping from DTO to Model
                var category = new Category
                {
                    Name = categoryDto.Name,
                    Description = categoryDto.Description,
                    TaxId = categoryDto.TaxId
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
                    TaxId = category.TaxId
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
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id);

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
                    TaxId = category.TaxId,
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
                var category = await _context.Categories.FindAsync(categoryDto.Id);
                if (category == null)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(404, "This category name already exists.");
                }

                // Check if the new category name already exists (case-insensitive), excluding the current category
                if (await _context.Categories.AnyAsync(c => c.Name.ToLower() == categoryDto.Name.ToLower() && c.Id != categoryDto.Id))
                {
                    return new ApiResponse<ConfirmationResponseDTO>(400, "Another category with the same name already exists.");
                }

                // Check if Category exists
                if (!await _context.Taxes.AnyAsync(cat => cat.Id == categoryDto.TaxId))
                {
                    return new ApiResponse<ConfirmationResponseDTO>(400, "The specified type of tax does not exist.");
                }

                // Update product properties manually
                category.Name = categoryDto.Name;
                category.Description = categoryDto.Description;
                category.TaxId = categoryDto.TaxId;

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

        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteCategoryAsync(CategoryDeleteDTO categoryDeleteDTO)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryDeleteDTO.Id);

                if (category == null)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(404, "This category was not found.");
                }

                // Implementing Delete
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                // Prepare confirmation message
                var confirmationMessage = new ConfirmationResponseDTO
                {
                    Message = $"Category with Id {categoryDeleteDTO.Id} deleted successfully."
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
                    .AsNoTracking()
                    .ToListAsync();

                var categoryList = categories.Select(c => new CategoryResponseDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    TaxId = c.TaxId,
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