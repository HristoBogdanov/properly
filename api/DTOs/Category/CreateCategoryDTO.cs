using System.ComponentModel.DataAnnotations;
using static api.Constants.CategoryErrorMessages;
using static api.Constants.CategoryValidationConstants;

namespace api.DTOs.Category
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = TitleRequired)]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = InvalidTitleLength)]
        public string Title { get; set; } = null!;
    }
}