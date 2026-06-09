using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; } // e.g., Cash, Credit Card, Debit Card, Mobile Payment
        public decimal AmountTendered { get; set; } // The amount of money given by the customer
        public decimal ChangeDue { get; set; } // The amount of money to return to the customer if they overpay

        // Navigation property: A payment is associated with one transaction
        public int TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public Transaction Transaction { get; set; }
    }
}