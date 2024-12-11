using System.ComponentModel.DataAnnotations;
using api.DTOs.Images;
using static api.Constants.FeatureErrorMessages;
using static api.Constants.FeatureValidationConstants;

namespace api.DTOs.Features
{
    public class CreateFeatureDTO
    {
        [Required(ErrorMessage = TitleRequired)]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = InvalidTitleLength)]
        public string Title { get; set; } = null!;
        public CreateImageDTO Image { get; set; } = null!;
    }
}