using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.Models
{
    public class PLU
    {
        public int Id { get; set; }
        public int PLUCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        // Navigation property: A PLU can be associated with one Tax
        public int TaxId { get; set; }
        [ForeignKey("TaxId")]
        public Tax Tax { get; set; }

        // Navigation property: A PLU belongs to one Department
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public ICollection<TransactionItem> TransactionItems { get; set; }
    }
}