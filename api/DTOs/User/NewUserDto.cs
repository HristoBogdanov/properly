using System.ComponentModel.DataAnnotations;
using api.Constants;

namespace api.Dtos.Account
{
    public class NewUserDto
    {
        [Required(ErrorMessage = ErrorMessages.UsernameRequired)]
        [StringLength(150, MinimumLength = 3, ErrorMessage = ErrorMessages.InvalidUsernameLength)]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = ErrorMessages.EmailRequired)]
        [EmailAddress(ErrorMessage = ErrorMessages.InvalidEmail)]
        public string Email { get; set; } = null!;

        [Required]
        public string Token { get; set; } = null!;
    }
}