using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.Models
{
    public class SaleItem
    {
        [Key]
        public long Id { get; set; }

        // Navigation Property: Multiple SaleItem can be associated with a single Transaction.
        public long TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        // // Navigation Property: Multiple SaleItem can be associated with a single Product.
        public int ProductId { get; set; }
        public Product Product { get; set; }


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