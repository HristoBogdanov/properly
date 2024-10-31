using System.ComponentModel.DataAnnotations;
using api.Constants;

namespace api.DTOs.User
{
    public class DeleteDTO
    {
        [Required(ErrorMessage = UserErrorMessages.UsernameRequired)]
        [StringLength(150, MinimumLength = 3, ErrorMessage = UserErrorMessages.InvalidUsernameLength)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = UserErrorMessages.EmailRequired)]
        [EmailAddress(ErrorMessage = UserErrorMessages.InvalidEmail)]
        public string Email { get; set; } = null!;
    }
}