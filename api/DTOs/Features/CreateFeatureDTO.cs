using System.ComponentModel.DataAnnotations;
using api.Constants;
using api.DTOs.Images;

namespace api.DTOs.Features
{
    public class CreateFeatureDTO
    {
        [Required(ErrorMessage = FeatureErrorMessages.TitleRequired)]
        [StringLength(200, MinimumLength =3, ErrorMessage = FeatureErrorMessages.InvalidTitleLength)]
        public string Title { get; set; } = null!;
        public DisplayImageDTO Image { get; set; } = null!;
    }
}