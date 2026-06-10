using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } // 1..16

        [MaxLength(200)]
        public string Description { get; set; }

        // Navigation Property: Multiple categories can be associated with a single tax.
        public int TaxId { get; set; }
        [ForeignKey("TaxId")]
        public Tax Tax { get; set; } = null!;
    }
}