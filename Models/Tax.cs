using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.Models
{
    public class Tax
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(6,2)")] // A decimal number with a precision of 6 and a scale of 2.
        public decimal Rate { get; set; } // e.g., 5% for 0.05
    }
}