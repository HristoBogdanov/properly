using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Images
{
    public class CreateImageDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Path { get; set; } = null!;
    }
}