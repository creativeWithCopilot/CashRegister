using System.ComponentModel.DataAnnotations;

namespace CashRegister.DTOs.TransactionDTOs
{
    public class TransactionResponseDTO
    {
        public long Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Subtotal { get; set; } // Sum of all LineTotal values across items.
        public decimal TaxTotal { get; set; } // Sum of all TaxAmount values across items.
        public decimal Total { get; set; } // Subtotal + TaxTotal
        public List<SaleItemResponseDTO> SaleItems { get; set; }
    }
}