using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.DTOs.ProductDTOs
{
    public class ProductResponseDTO
    {
        public string PLUCode { get; set; } // A 4-digit PLU code is represented as a 5-digit code by adding a leading zero.
        public string Description { get; set; }
        public decimal Price { get; set; }

        // Navigation Property: Multiple Product can be associated with a single category.
        public int CategoryId { get; set; } // Required foreign key property

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}