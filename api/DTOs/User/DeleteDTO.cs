using System.ComponentModel.DataAnnotations;
using static api.Constants.UserErrorMessages;
using static api.Constants.UserValidationConstants;

namespace api.DTOs.User
{
    public class DeleteDTO
    {
        [Required(ErrorMessage = UsernameRequired)]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength, ErrorMessage = InvalidUsernameLength)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = EmailRequired)]
        [EmailAddress(ErrorMessage = InvalidEmail)]
        public string Email { get; set; } = null!;
    }
}