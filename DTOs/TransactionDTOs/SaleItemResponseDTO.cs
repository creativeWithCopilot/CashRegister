using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.DTOs.TransactionDTOs
{
    public class SaleItemResponseDTO
    {
        public long Id { get; set; }
        public string PLUCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; } // UnitPrice x Quantity
    }
}