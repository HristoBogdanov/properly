using api.DTOs.User;

namespace api.Services.Interfaces
{
    public interface IUserService
    {
        Task<NewUserDTO> Login(LoginDTO loginDto);
        Task<NewUserDTO> Register(RegisterDTO registerDto);
        Task<NewUserDTO> RegisterBroker(RegisterDTO registerDto);
        Task<NewUserDTO> RegisterAdmin(RegisterDTO registerDto);
        Task<DeleteDTO> DeleteUser(Guid id);
    }
}