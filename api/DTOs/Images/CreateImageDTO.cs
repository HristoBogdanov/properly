using System.ComponentModel.DataAnnotations;
using static api.Constants.ImageErrorMessages;
using static api.Constants.ImageValidationConstants;

namespace api.DTOs.Images
{
    public class CreateImageDTO
    {
        [Required(ErrorMessage = ImageNameRequired)]
        [StringLength(ImageNameMaxLength, MinimumLength = ImageNameMinLength, ErrorMessage = InvalidImageNameLength)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = ImagePathRequired)]
        [StringLength(ImagePathMaxLength, MinimumLength = ImagePathMinLength, ErrorMessage = InvalidImagePathLength)]
        public string Path { get; set; } = null!;
    }
}