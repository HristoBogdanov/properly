using System.ComponentModel.DataAnnotations;
using static api.Constants.UserErrorMessages;
using static api.Constants.UserValidationConstants;

namespace api.DTOs.User
{
    public class LoginDTO
    {
        [Required(ErrorMessage = UsernameRequired)]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength, ErrorMessage = InvalidUsernameLength)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = PasswordRequired)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = InvalidPasswordLength)]
        public string Password { get; set; } = null!;
    }
}