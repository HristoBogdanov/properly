using api.Dtos.Account;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class ApplicationUserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public ApplicationUserController(
        IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userRepository.Login(loginDto);
                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userRepository.Register(registerDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // TO DO: REMOVE THIS AND ADD AN ADMIN THROUGH SEED
        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var admin = await _userRepository.RegisterAdmin(registerDto);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register-broker")]
        public async Task<IActionResult> RegisterBroker(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var broker = await _userRepository.RegisterBroker(registerDto);
                return Ok(broker);
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
                await _userRepository.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
