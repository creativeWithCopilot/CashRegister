using Microsoft.EntityFrameworkCore;
using CashRegister.Data;
using CashRegister.DTOs;
using CashRegister.DTOs.TransactionDTOs;
using CashRegister.Models;

namespace CashRegister.Services
{
    public class TransactionService
    {
        private readonly ApplicationDbContext _context;

        // Inject the ApplicationDbContext.
        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<TransactionResponseDTO>> CreateTransactionAsync(TransactionCreateDTO transactionDto)
        {
            try
            {
                // Initialize financial tracking.
                decimal subtotal = 0;
                decimal taxToal = 0;
                decimal total = 0;

                // List to hold transaction items.
                var saleItems = new List<SaleItem>();

                // Process each transaction item from the DTO.
                foreach (var saleItemDto in transactionDto.SaleItems)
                {
                    // Check if the product exists.
                    var product = await _context.Products.FindAsync(saleItemDto.PLUCode);
                    if (product == null)
                    {
                        return new ApiResponse<TransactionResponseDTO>(404, $"Product with ID {saleItemDto.PLUCode} does not exist.");
                    }

                    // Calculate base price, tax, and total price for the order item.
                    decimal basePrice = saleItemDto.Quantity * product.Price;

                    // Create a new SaleItem.
                    var saleItem = new SaleItem
                    {
                        PLUCode = product.PLUCode,
                        Quantity = saleItemDto.Quantity,
                        UnitPrice = product.Price,
                        LineTotal = basePrice
                    };

                    // Add the transaction item to the list.
                    saleItems.Add(saleItem);

                    // Update the running totals.
                    subtotal += basePrice;
                }

                // Calculate the final total amount.
                taxToal = subtotal * 0.05m;
                total = subtotal + taxToal;

                // Manually map from DTO to Order model.
                var transaction = new Transaction
                {
                    OrderDate = DateTime.Now,
                    Subtotal = subtotal,
                    TaxTotal = taxToal,
                    Total = total,
                    SaleItems = saleItems
                };

                // Add the order to the database.
                _context.Transactions.Add(transaction);
                // Save all changes.
                await _context.SaveChangesAsync();
                
                // Map the saved order to OrderResponseDTO.
                var transactionResponse = MapTransactionToDTO(transaction);
                return new ApiResponse<TransactionResponseDTO>(200, transactionResponse);
            }
            catch (Exception ex)
            {
                return new ApiResponse<TransactionResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        // Retrieves an Transactions by its ID along with related entities.
        public async Task<ApiResponse<TransactionResponseDTO>> GetTransactionByIdAsync(int transactionId)
        {
            try
            {
                // Retrieve the Transaction with its items details.
                var transaction = await _context.Transactions
                    .Include(t => t.SaleItems)
                        .ThenInclude(si => si.Product)
                    .FirstOrDefaultAsync(t => t.Id == transactionId);

                if (transaction == null)
                {
                    return new ApiResponse<TransactionResponseDTO>(404, "Transaction not found.");
                }

                // Map the Transaction to a DTO.
                var transactionResponse = MapTransactionToDTO(transaction);
                return new ApiResponse<TransactionResponseDTO>(200, transactionResponse);
            }
            catch (Exception ex)
            {
                return new ApiResponse<TransactionResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        // Retrieves all orders in the system.
        public async Task<ApiResponse<List<TransactionResponseDTO>>> GetAllTransactionAsync()
        {
            try
            {
                // Retrieve all orders with related entities.
                var transaction = await _context.Transactions
                    .Include(t => t.SaleItems)
                        .ThenInclude(si => si.Product)
                    .AsNoTracking()
                    .ToListAsync();

                // Map each order to its corresponding DTO.
                var transactionList = transaction.Select(t => MapTransactionToDTO(t)).ToList();
                return new ApiResponse<List<TransactionResponseDTO>>(200, transactionList);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<TransactionResponseDTO>>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }
        
         #region Helper Methods

        // Maps an Transaction entity to an TransactionResponseDTO.
        private TransactionResponseDTO MapTransactionToDTO(Transaction transaction)
        {
            // Map Transaction items.
            var saleItemsDto = transaction.SaleItems.Select(ti => new SaleItemResponseDTO
            {
                Id = ti.Id,
                PLUCode = ti.PLUCode,
                Quantity = ti.Quantity,
                UnitPrice = ti.UnitPrice,
                LineTotal = ti.LineTotal
            }).ToList();

            // Create and return the DTO.
            return new TransactionResponseDTO
            {
                Id = transaction.Id,
                OrderDate = transaction.OrderDate,
                Subtotal = transaction.Subtotal,
                TaxTotal = transaction.TaxTotal,
                Total = transaction.Total,
                SaleItems = saleItemsDto
            };
        }
        #endregion
    }
}