using System.ComponentModel.DataAnnotations;
using api.Constants;

namespace api.Dtos.Account
{
    public class LoginDto
    {
        [Required(ErrorMessage = ErrorMessages.UsernameRequired)]
        [StringLength(150, MinimumLength = 3, ErrorMessage = ErrorMessages.InvalidUsernameLength)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = ErrorMessages.PasswordRequired)]
        [StringLength(150, MinimumLength =12, ErrorMessage = ErrorMessages.InvalidPasswordLength)]
        public string Password { get; set; } = null!;
    }
}