using System.ComponentModel.DataAnnotations;
using api.Constants;

namespace api.DTOs.Category
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = CategoryErrorMessages.TitleRequired)]
        [StringLength(200, MinimumLength = 3, ErrorMessage = CategoryErrorMessages.InvalidTitleLength)]
        public string Title = null!;
    }
}