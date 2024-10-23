using api.Dtos.Account;
using api.DTOs.User;
using api.Models;

namespace api.Interfaces
{
    public interface IUserRepository
    {
        Task<NewUserDto> Login(LoginDto loginDto);
        Task<NewUserDto> Register(RegisterDto registerDto);
        Task<NewUserDto> RegisterBroker(RegisterDto registerDto);
        Task<NewUserDto> RegisterAdmin(RegisterDto registerDto);
        Task<DeleteDTO> DeleteUser(Guid id);
    }
}