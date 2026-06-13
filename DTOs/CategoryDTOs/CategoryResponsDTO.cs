namespace CashRegister.DTOs.CategoryDTOs
{
    public class CategoryResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        // Navigation Property: Multiple categories can be associated with a single tax.
        public int TaxId { get; set; } // Required foreign key property
    }
}