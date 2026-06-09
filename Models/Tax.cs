namespace CashRegister.Models
{
    public class Tax
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        
        // Navigation property: A tax can be associated with multiple PLUs
        public ICollection<PLU> PLUs { get; set; }
    }
}