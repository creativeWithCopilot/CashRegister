using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.Models
{
    public class TransactionItem
    {
        public int Id { get; set; }

        // Navigation property: A transaction item belongs to one transaction
        public int TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public Transaction Transaction { get; set; }

        // Navigation property: A transaction item is associated with one PLU
        public int PLUId { get; set; }
        [ForeignKey("PLUId")]
        public PLU PLU { get; set;}

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } // Captured at the time of transaction to account for price changes
        public decimal LineTotal { get; set; } // Calculated as Quantity * UnitPrice

    }
}