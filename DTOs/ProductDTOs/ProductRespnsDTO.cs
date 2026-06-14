using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.DTOs.ProductDTOs
{
    public class ProductResponseDTO
    {
        public string PLUCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive {get; set; }

        // Navigation Property: Multiple Product can be associated with a single category.
        public int CategoryId { get; set; }
    }
}