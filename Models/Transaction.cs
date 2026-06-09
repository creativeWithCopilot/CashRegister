namespace CashRegister.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Subtotal { get; set; } // Calculated as the sum of LineTotals of all TransactionItems
        public decimal TaxTotal { get; set; } // Calculated as the sum of taxes for all TransactionItems
        public decimal GrandTotal { get; set; } // Calculated as Subtotal + TaxTotal

        // Navigation property: A transaction can have multiple transaction items
        public ICollection<TransactionItem> TransactionItems { get; set; }
        
        // Navigation property: A transaction can have one payment
        public Payment Payment { get; set; }
    }
}