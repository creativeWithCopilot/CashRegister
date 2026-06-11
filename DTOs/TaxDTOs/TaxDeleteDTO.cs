using System.ComponentModel.DataAnnotations;

namespace CashRegister.DTOs.TaxDTOs
{
    public class TaxDeleteDTO
    {
        [Required(ErrorMessage = "The tax Id is required.")]
        public int Id { get; set; }
    }
}