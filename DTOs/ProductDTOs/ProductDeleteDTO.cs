using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.DTOs.ProductDTOs
{
    public class ProductDeleteDTO
    {
        [Required(ErrorMessage = "A PLU code is necessary.")]
        [Range(00000, 99999, ErrorMessage = "PLU codes must consist of 5 digits. A 4-digit PLU code is represented as a 5-digit code by adding a leading zero.")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PLUCode { get; set; } // A 4-digit PLU code is represented as a 5-digit code by adding a leading zero.
    }
}