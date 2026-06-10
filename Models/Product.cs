using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.Models
{
    public class Product
    {
        [Key]
        [Required]
        [Range(00000, 99999)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PLUCode { get; set; } // A 4-digit PLU code is represented as a 5-digit code by adding a leading zero.

        [MaxLength(200)]
        public string Description { get; set; }


        [Column(TypeName = "decimal(6,2)")] // A decimal number with a precision of 6 and a scale of 2.
        public decimal Price { get; set; }

        // Navigation Property: Multiple Product can be associated with a single category.
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;
    }
}