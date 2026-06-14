using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.DTOs.TransactionDTOs
{
    public class TransactionCreateDTO
    {
        [Required(ErrorMessage = "At least one order item is required.")]
        [MinLength(1, ErrorMessage = "At least one order item is required.")]
        public List<SaleItemCreateDTO> SaleItems { get; set; }
    }
}