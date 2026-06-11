using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.DTOs.TaxDTOs
{
    public class TaxResponseDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Rate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}