using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.DTOs.TaxDTOs
{
    public class TaxUpdateDTO
    {
        [Required(ErrorMessage = "The tax Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The type of tax is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The type of tax must be between 3 and 50 characters long.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "The tax rate must be specified.")]
        [Range(0.01, 100.00, ErrorMessage = "The tax rate must be between $0.01 and $100.00.")]
        [Column(TypeName = "decimal(5,2)")] // A decimal number with a precision of 6 and a scale of 2.
        public decimal Rate { get; set; } // e.g., 5% for 0.05
        
        public bool IsActive {get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}