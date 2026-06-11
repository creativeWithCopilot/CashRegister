using System.ComponentModel.DataAnnotations;

namespace CashRegister.DTOs.CategoryDTOs
{
    public class CategoryDeleteDTO
    {
        [Required(ErrorMessage = "The category ID is required.")]
        public int Id { get; set; }
    }
}