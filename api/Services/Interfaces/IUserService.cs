using api.DTOs.User;

namespace api.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<DisplayUserDTO>> GetUsersAsync();
        Task<DisplayUserDTO> GetUserByIdAsync(string id);
        Task<NewUserDTO> Login(LoginDTO loginDto);
        Task<NewUserDTO> Register(RegisterDTO registerDto);
        Task<NewUserDTO> RegisterAdmin(RegisterDTO registerDto);
        Task<bool> DeleteUser(Guid id);
    }
}