using System.ComponentModel.DataAnnotations;

namespace mlsaDemo.DTO
{
    public class CreateItemDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
