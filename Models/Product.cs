using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.Models
{
    public class Product
    {
        [Key]
        [Required]
        [Range(00000, 99999, ErrorMessage = "PLU codes must consist of 5 digits. A 4-digit PLU code is represented as a 5-digit code by adding a leading zero.")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PLUCode { get; set; } // A 4-digit PLU code is represented as a 5-digit code by adding a leading zero.

        [MaxLength(200, ErrorMessage = "The description must be no more than 200 characters.")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, 1000000.00, ErrorMessage = "Price must be between $0.01 and $100,000.00.")]
        [Column(TypeName = "decimal(10,2)")] // A decimal number with a precision of 6 and a scale of 2.
        public decimal Price { get; set; }

        // Navigation Property: Multiple Product can be associated with a single category.
        public int CategoryId { get; set; } // Required foreign key property
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!; // Required reference navigation to principal

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}