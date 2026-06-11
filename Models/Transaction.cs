using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.Models
{
    public class Transaction
    {
        [Key]
        public long Id { get; set; }

        public DateTime Timestamp { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; } // Sum of all LineTotal values across items.

        [Column(TypeName = "decimal(10,2)")]
        public decimal TaxTotal { get; set; } // Sum of all TaxAmount values across items.

        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; } // Subtotal + TaxTotal

        [Required(ErrorMessage = "The payment type is required.")]
        public PaymentType Payment { get; set; }

        // Navigation Property: A single Transaction can be linked to multiple SaleItem.
        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>(); // Collection navigation containing dependents

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Timestamp] // <-- RowVersion for concurrency
        public byte[] RowVersion { get; set; }
    }
}