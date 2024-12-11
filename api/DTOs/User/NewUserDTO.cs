using System.ComponentModel.DataAnnotations;
using static api.Constants.UserErrorMessages;
using static api.Constants.UserValidationConstants;
using static api.Constants.Regexes;

namespace api.DTOs.User
{
    public class NewUserDTO
    {
        [Required(ErrorMessage = UsernameRequired)]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength, ErrorMessage = InvalidUsernameLength)]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = EmailRequired)]
        [EmailAddress(ErrorMessage = InvalidEmail)]
        public string Email { get; set; } = null!;

        [Required]
        public string Token { get; set; } = null!;

        [Required(ErrorMessage = RoleRequired)]
        [RegularExpression(RolesRegex, ErrorMessage = InvalidRole)]
        public string Role { get; set; } = null!;
    }
}