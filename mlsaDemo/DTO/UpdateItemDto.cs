using System.ComponentModel.DataAnnotations;

namespace mlsaDemo.DTO
{
    public class UpdateItemDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be more then 100")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
