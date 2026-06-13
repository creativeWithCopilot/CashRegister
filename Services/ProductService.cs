using Microsoft.EntityFrameworkCore;
using CashRegister.Data;
using CashRegister.DTOs.ProductDTOs;
using CashRegister.Models;
using CashRegister.DTOs;

namespace CashRegister.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<ProductResponseDTO>> CreateProductAsync(ProductCreateDTO productDto)
        {
            try
            {
                // Check if the PLU code already exists
                if (await _context.Products.AnyAsync(p => p.PLUCode == productDto.PLUCode))
                {
                    return new ApiResponse<ProductResponseDTO>(400, "The PLU code already exists.");
                }

                // Check if Category exists
                if (!await _context.Categories.AnyAsync(cat => cat.Id == productDto.CategoryId))
                {
                    return new ApiResponse<ProductResponseDTO>(400, "This category was not found.");
                }

                // Manual mapping from DTO to Model
                var product = new Product
                {
                    PLUCode = productDto.PLUCode,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    CategoryId = productDto.CategoryId,
                    CreatedAt = DateTime.Now
                };

                // Add product to the database
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                // Map to ProductResponseDTO
                var productResponse = new ProductResponseDTO
                {
                    PLUCode = product.PLUCode,
                    Description = product.Description,
                    Price = product.Price,
                    IsActive = product.IsActive,
                    CategoryId = product.CategoryId,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt
                };

                return new ApiResponse<ProductResponseDTO>(200, productResponse);
            }
            catch (Exception ex)
            {
                // Log the exception (implementation depends on your logging setup)
                return new ApiResponse<ProductResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ProductResponseDTO>> GetProductByIdAsync(string pluCode)
        {
            try
            {
                var product = await _context.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.PLUCode == pluCode);

                if (product == null)
                {
                    return new ApiResponse<ProductResponseDTO>(404, "Product not found.");
                }

                // Map to ProductResponseDTO
                var productResponse = new ProductResponseDTO
                {
                    PLUCode = product.PLUCode,
                    Description = product.Description,
                    Price = product.Price,
                    IsActive = product.IsActive,
                    CategoryId = product.CategoryId,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt
                };

                return new ApiResponse<ProductResponseDTO>(200, productResponse);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ApiResponse<ProductResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateProductAsync(ProductUpdateDTO productDto)
        {
            try
            {
                var product = await _context.Products.FindAsync(productDto.PLUCode);
                if (product == null)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(404, "Product not found.");
                }

                // Check if the new PLU code already exists, excluding the current product
                if (await _context.Products.AnyAsync(p => p.PLUCode != productDto.PLUCode))
                {
                    return new ApiResponse<ConfirmationResponseDTO>(400, "Another product with the same PLU code already exists.");
                }

                // Check if Category exists
                if (!await _context.Categories.AnyAsync(cat => cat.Id == productDto.CategoryId))
                {
                    return new ApiResponse<ConfirmationResponseDTO>(400, "Specified category does not exist.");
                }

                // Update product properties manually
                product.PLUCode = productDto.PLUCode;
                product.Description = productDto.Description;
                product.Price = productDto.Price;
                product.IsActive = productDto.IsActive;
                product.CategoryId = productDto.CategoryId;
                product.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();

                // Prepare confirmation message
                var confirmationMessage = new ConfirmationResponseDTO
                {
                    Message = $"Product with PLU code {productDto.PLUCode} updated successfully."
                };

                return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ApiResponse<ConfirmationResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteProductAsync(string pluCode)
        {
            try
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.PLUCode == pluCode);

                if (product == null)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(404, "Product not found.");
                }

                // Implementing Soft Delete
                product.IsActive = false;
                await _context.SaveChangesAsync();

                // Prepare confirmation message
                var confirmationMessage = new ConfirmationResponseDTO
                {
                    Message = $"Product with PLU code {pluCode} deleted successfully."
                };

                return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ApiResponse<ConfirmationResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<ProductResponseDTO>>> GetAllProductsAsync()
        {
            try
            {
                var products = await _context.Products
                    .AsNoTracking()
                    .ToListAsync();

                var productList = products.Select(p => new ProductResponseDTO
                {
                    PLUCode = p.PLUCode,
                    Description = p.Description,
                    Price = p.Price,
                    IsActive = p.IsActive,
                    CategoryId = p.CategoryId,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                }).ToList();

                return new ApiResponse<List<ProductResponseDTO>>(200, productList);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ApiResponse<List<ProductResponseDTO>>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<ProductResponseDTO>>> GetAllProductsByCategoryAsync(int categoryId)
        {
            try
            {
                // Retrieve products associated with the specified category
                var products = await _context.Products
                    .AsNoTracking()
                    .Include(p => p.Category)
                    .Where(p => p.CategoryId == categoryId)
                    .ToListAsync();

                if (products == null || products.Count == 0)
                {
                    return new ApiResponse<List<ProductResponseDTO>>(404, "Products not found.");
                }

                var productList = products.Select(p => new ProductResponseDTO
                {
                    PLUCode = p.PLUCode,
                    Description = p.Description,
                    Price = p.Price,
                    IsActive = p.IsActive,
                    CategoryId = p.CategoryId,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                }).ToList();

                return new ApiResponse<List<ProductResponseDTO>>(200, productList);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ApiResponse<List<ProductResponseDTO>>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }
    }
}