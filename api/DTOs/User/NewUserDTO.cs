using System.ComponentModel.DataAnnotations;
using api.Constants;

namespace api.DTOs.User
{
    public class NewUserDTO
    {
        [Required(ErrorMessage = UserErrorMessages.UsernameRequired)]
        [StringLength(150, MinimumLength = 3, ErrorMessage = UserErrorMessages.InvalidUsernameLength)]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = UserErrorMessages.EmailRequired)]
        [EmailAddress(ErrorMessage = UserErrorMessages.InvalidEmail)]
        public string Email { get; set; } = null!;

        [Required]
        public string Token { get; set; } = null!;

        [Required(ErrorMessage = UserErrorMessages.RoleRequired)]
        [RegularExpression(Regexes.RolesRegex, ErrorMessage = UserErrorMessages.InvalidRole)]
        public string Role { get; set; } = null!;
    }
}