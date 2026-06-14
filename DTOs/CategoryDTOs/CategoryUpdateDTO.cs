using System.ComponentModel.DataAnnotations;

namespace CashRegister.DTOs.CategoryDTOs
{
    public class CategoryUpdateDTO
    {
        [Required(ErrorMessage = "The category ID is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The category must be defined.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The category musb be between 3 and 50 characters.")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "The description must be no more than 200 characters.")]
        public string Description { get; set; }
        
        public bool IsActive { get; set; }
    }
}