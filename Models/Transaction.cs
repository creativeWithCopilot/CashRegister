using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.Models
{
    public class Transaction
    {
        [Key]
        public long Id { get; set; }

        public DateTime Timestamp { get; set; }

        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Subtotal { get; set; } // Sum of all LineTotal values across items.

        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal TaxTotal { get; set; } // Sum of all TaxAmount values across items.

        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Total { get; set; } // Subtotal + TaxTotal

        [Required]
        public PaymentType Payment { get; set; }

        // Navigation Property: A single Transaction can be linked to multiple SaleItem.
        [Required]
        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>(); // Collection navigation containing dependents

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Timestamp] // <-- RowVersion for concurrency
        public byte[] RowVersion { get; set; }
    }
}