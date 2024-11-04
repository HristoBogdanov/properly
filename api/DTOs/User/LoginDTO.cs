using System.ComponentModel.DataAnnotations;
using api.Constants;

namespace api.DTOs.User
{
    public class LoginDTO
    {
        [Required(ErrorMessage = UserErrorMessages.UsernameRequired)]
        [StringLength(150, MinimumLength = 3, ErrorMessage = UserErrorMessages.InvalidUsernameLength)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = UserErrorMessages.PasswordRequired)]
        [StringLength(150, MinimumLength =12, ErrorMessage = UserErrorMessages.InvalidPasswordLength)]
        public string Password { get; set; } = null!;
    }
}