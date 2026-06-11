using Microsoft.EntityFrameworkCore;
using CashRegister.Data;
using CashRegister.DTOs.TaxDTOs;
using CashRegister.Models;
using CashRegister.DTOs;

namespace CashRegister.Services
{
    public class TaxService
    {
        private readonly ApplicationDbContext _context;

        public TaxService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<TaxResponseDTO>> CreateTaxAsync(TaxCreateDTO taxDto)
        {
            try
            {
                // Check if tax type already exists (case-insensitive)
                if (await _context.Taxes.AnyAsync(t => t.Type.ToLower() == taxDto.Type.ToLower()))
                {
                    return new ApiResponse<TaxResponseDTO>(400, "This type of tax already exists.");
                }

                // Manual mapping from DTO to Model
                var tax = new Tax
                {
                    Type = taxDto.Type,
                    Rate = taxDto.Rate,
                    CreatedAt = DateTime.Now
                };

                // Add tax to the database
                _context.Taxes.Add(tax);
                await _context.SaveChangesAsync();

                // Map to TaxResponseDTO
                var taxResponse = new TaxResponseDTO
                {
                    Id = tax.Id,
                    Type = tax.Type,
                    Rate = tax.Rate,
                    CreatedAt = tax.CreatedAt,
                    UpdatedAt = tax.UpdatedAt
                };

                return new ApiResponse<TaxResponseDTO>(200, taxResponse);
            }
            catch (Exception ex)
            {
                // Log the exception (implementation depends on your logging setup)
                return new ApiResponse<TaxResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<TaxResponseDTO>> GetTaxByIdAsync(int id)
        {
            try
            {
                var tax = await _context.Taxes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (tax == null)
                {
                    return new ApiResponse<TaxResponseDTO>(404, "The type of tax is not found.");
                }

                // Map to TaxResponseDTO
                var taxResponseDTO = new TaxResponseDTO
                {
                    Id = tax.Id,
                    Type = tax.Type,
                    Rate = tax.Rate,
                    CreatedAt = tax.CreatedAt,
                    UpdatedAt = tax.UpdatedAt
                };

                return new ApiResponse<TaxResponseDTO>(200, taxResponseDTO);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ApiResponse<TaxResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateTaxAsync(TaxUpdateDTO taxDto)
        {
            try
            {
                var tax = await _context.Taxes.FindAsync(taxDto.Id);
                if (tax == null)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(404, "The type of tax is not found.");
                }

                // Check if the new tax name already exists (excluding current category)
                if (await _context.Taxes.AnyAsync(t => t.Type.ToLower() == taxDto.Type.ToLower() && t.Id != taxDto.Id))
                {
                    return new ApiResponse<ConfirmationResponseDTO>(400, "Another type of tax with the same tax already exists.");
                }

                // Update tax properties manually
                tax.Type = taxDto.Type;
                tax.Rate = taxDto.Rate;
                tax.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();

                // Prepare confirmation message
                var confirmationMessage = new ConfirmationResponseDTO
                {
                    Message = $"Tax with Id {taxDto.Id} updated successfully."
                };

                return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ApiResponse<ConfirmationResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteTaxAsync(TaxDeleteDTO  taxDeleteDTO)
        {
            try
            {
                var tax = await _context.Taxes.FirstOrDefaultAsync(t => t.Id == taxDeleteDTO.Id);

                if (tax == null)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(404, "The type of tax is not found.");
                }

                _context.Taxes.Remove(tax);
                await _context.SaveChangesAsync();

                // Prepare confirmation message
                var confirmationMessage = new ConfirmationResponseDTO
                {
                    Message = $"Tax with Id {taxDeleteDTO.Id} deleted successfully."
                };

                return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ApiResponse<ConfirmationResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<TaxResponseDTO>>> GetAllTaxAsync()
        {
            try
            {
                var taxes = await _context.Taxes
                    .AsNoTracking()
                    .ToListAsync();

                var taxList = taxes.Select(t => new TaxResponseDTO
                {
                    Id = t.Id,
                    Type = t.Type,
                    Rate = t.Rate,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt
                }).ToList();

                return new ApiResponse<List<TaxResponseDTO>>(200, taxList);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ApiResponse<List<TaxResponseDTO>>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }
    }
}