using api.Dtos.Account;

namespace api.Interfaces
{
    public interface IUserRepository
    {
        Task<NewUserDto> Login(LoginDto loginDto);
        Task<NewUserDto> Register(RegisterDto registerDto);
    }
}