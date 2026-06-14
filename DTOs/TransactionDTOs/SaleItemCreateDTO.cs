using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.DTOs.TransactionDTOs
{
    public class SaleItemCreateDTO
    {
        // Navigation Property: Multiple SaleItem can be associated with a single Product.
        [Required]
        public string PLUCode { get; set; }

        [Required(ErrorMessage = "At least one order item is required.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Quantity { get; set; } // supports decimal quantities (weights)]
    }
}