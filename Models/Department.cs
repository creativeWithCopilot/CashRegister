namespace CashRegister.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        // Navigation property: A department can have multiple PLUs
        public ICollection<PLU> PLUs { get; set; }
    }
}