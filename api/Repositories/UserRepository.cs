using api.Constants;
using api.Dtos.Account;
using api.DTOs.User;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly ITokenService _tokenService;
        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signinManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<NewUserDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) throw new UnauthorizedAccessException(UserErrorMessages.InvalidUsernameOrPassword);

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) throw new UnauthorizedAccessException(UserErrorMessages.InvalidUsernameOrPassword);

            return new NewUserDto
                {
                    UserName = user.UserName!,
                    Email = user.Email!,
                    Token = _tokenService.CreateToken(user)
                };
        }

        public async Task<NewUserDto> Register(RegisterDto registerDto)
        {
            var appUser = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded)
                {
                    return new NewUserDto
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser)
                    };
                }
                else
                {
                    throw new Exception(string.Join("\n", roleResult.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                throw new Exception(string.Join("\n", createdUser.Errors.Select(e => e.Description)));
            }
        }

        public async Task<NewUserDto> RegisterBroker(RegisterDto registerDto)
        {
            var broker = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(broker, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            await _userManager.AddToRoleAsync(broker, "Broker");
            return new NewUserDto {
                UserName = broker.UserName,
                Email = broker.Email,
                Token = _tokenService.CreateToken(broker)
            };
        }

        // TODO: Remove this and add the admin using seed
        public async Task<NewUserDto> RegisterAdmin(RegisterDto registerDto)
        {
            var admin = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(admin, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            await _userManager.AddToRoleAsync(admin, "Admin");
            return new NewUserDto
                {
                    UserName = admin.UserName,
                    Email = admin.Email,
                    Token = _tokenService.CreateToken(admin)
                };
        }

        public async Task<DeleteDTO> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new Exception("User not found");
            }

            await _userManager.DeleteAsync(user);

            return new DeleteDTO
            {
                Username = user.UserName!,
                Email = user.Email!
            };
        }
    }
}