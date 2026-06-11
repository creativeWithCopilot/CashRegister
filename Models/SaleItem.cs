using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.Models
{
    public class SaleItem
    {
        [Key]
        public long Id { get; set; }

        // Navigation Property: Multiple SaleItem can be associated with a single Transaction.
        [Required]
        public long TransactionId { get; set; } // Required foreign key property
        [ForeignKey("TransactionId")]
        public Transaction Transaction { get; set; } = null!; // Required reference navigation to principal
        // Navigation Property: Multiple SaleItem can be associated with a single Product.
        [Required]
        public int ProductId { get; set; } // Required foreign key property
        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!; // Required reference navigation to principal

        [Required]
        [Range(1,100)]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Quantity { get; set; } // supports decimal quantities (weights)

        [Column(TypeName = "decimal(6,2)")]
        public decimal LineTotal { get; set; } // UnitPrice x Quantity

        [Column(TypeName = "decimal(6,2)")]
        public decimal TaxAmount { get; set; } // LineTotal × TaxRate

        [Timestamp] // <-- RowVersion for concurrency
        public byte[] RowVersion { get; set; }
    }
}