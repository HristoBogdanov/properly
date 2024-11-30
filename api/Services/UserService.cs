using api.Constants;
using api.Data.Repository.Interfaces;
using api.DTOs.User;
using api.Interfaces;
using api.Models;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly ITokenService _tokenService;
        private readonly IRepository<Property, Guid> _propertyRepository;
        public UserService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService,
        IRepository<Property, Guid> propertyRepository)
        {
            _userManager = userManager;
            _signinManager = signInManager;
            _tokenService = tokenService;
            _propertyRepository = propertyRepository;
        }

        public async Task<List<DisplayUserDTO>> GetUsersAsync()
        {
            var users = await _userManager.Users
                .Select(user => new DisplayUserDTO
                {
                    Id = user.Id.ToString(),
                    Username = user.UserName!,
                    Email = user.Email!
                })
                .AsNoTracking()
                .ToListAsync();

            foreach (var user in users)
            {
                var appUser = await _userManager.FindByIdAsync(user.Id);
                user.Role = (await _userManager.GetRolesAsync(appUser!)).FirstOrDefault()!;
                int propertyCount = _propertyRepository.GetAll().Count(p => p.OwnerId == Guid.Parse(user.Id));
                user.NumberOfProperties = propertyCount;
            }

            return users;
        }

        public async Task<DisplayUserDTO> GetUserByIdAsync(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id.ToString() == id);

            if (user == null)
            {
                throw new Exception(UserErrorMessages.UserNotFound);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            var userDTO = new DisplayUserDTO
            {
                Id = user.Id.ToString(),
                Username = user.UserName!,
                Email = user.Email!,
                Role = role!,
                NumberOfProperties = _propertyRepository.GetAll().Count(p => p.OwnerId == user.Id)
            };

            return userDTO;
        }

        public async Task<NewUserDTO> Login(LoginDTO loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) throw new UnauthorizedAccessException(UserErrorMessages.InvalidUsernameOrPassword);

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) throw new UnauthorizedAccessException(UserErrorMessages.InvalidUsernameOrPassword);

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            if (role == null)
            {
                role = "User";
            }

            return new NewUserDTO
            {
                UserName = user.UserName!,
                Email = user.Email!,
                Token = _tokenService.CreateToken(user),
                Role = role,
            };
        }

        public async Task<NewUserDTO> Register(RegisterDTO registerDto)
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
                    return new NewUserDTO
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser),
                        Role = "User"
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

        public async Task<NewUserDTO> RegisterAdmin(RegisterDTO registerDto)
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
            return new NewUserDTO
            {
                UserName = admin.UserName,
                Email = admin.Email,
                Token = _tokenService.CreateToken(admin),
                Role = "Admin"
            };
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new Exception(UserErrorMessages.UserNotFound);
            }

            var properties = _propertyRepository.GetAll().Where(p => p.OwnerId == user.Id).ToList();
            foreach (var property in properties)
            {
                await _propertyRepository.DeleteAsync(property);
            }
            await _userManager.DeleteAsync(user);

            return true;
        }
    }
}