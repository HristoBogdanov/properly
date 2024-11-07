using api.DTOs.User;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IUserService _userService;

        public ApplicationUserController(
        IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            try
            {
                var user = await _userService.Login(loginDto);
                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            try
            {
                var user = await _userService.Register(registerDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register-broker")]
        public async Task<IActionResult> RegisterBroker(RegisterDTO registerDto)
        {
            try
            {
                var broker = await _userService.RegisterBroker(registerDto);
                return Ok(broker);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // TO DO: REMOVE THIS AND ADD AN ADMIN THROUGH SEED
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterDTO registerDto)
        {
            try
            {
                var admin = await _userService.RegisterAdmin(registerDto);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
