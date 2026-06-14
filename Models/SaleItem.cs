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
        public string PLUCode { get; set; } // Required foreign key property
        [ForeignKey("PLUCode")]
        public Product Product { get; set; } = null!; // Required reference navigation to principal

        [Range(1,100)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Quantity { get; set; } // supports decimal quantities (weights)

        [Range(0.00, 100000.00, ErrorMessage = "Unit Price must be between $0.00 and $100,000.00.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal LineTotal { get; set; } // UnitPrice x Quantity
    }
}