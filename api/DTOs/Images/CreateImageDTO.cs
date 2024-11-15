using System.ComponentModel.DataAnnotations;
using api.Constants;

namespace api.DTOs.Images
{
    public class CreateImageDTO
    {
        [Required(ErrorMessage = ImageErrorMessages.ImageNameRequired)]
        [StringLength(100, MinimumLength = 3, ErrorMessage = ImageErrorMessages.InvalidImageNameLength)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = ImageErrorMessages.ImagePathRequired)]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = ImageErrorMessages.InvalidImagePathLength)]
        public string Path { get; set; } = null!;
    }
}