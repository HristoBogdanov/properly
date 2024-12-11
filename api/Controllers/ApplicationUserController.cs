using api.DTOs.User;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller for managing application users.
/// </summary>
[ApiController]
[Route("api/account")]
public class ApplicationUserController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationUserController"/> class.
    /// </summary>
    /// <param name="userService">The user service.</param>
    public ApplicationUserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Gets the list of all users.
    /// </summary>
    /// <returns>A list of users.</returns>
    [HttpGet("users")]
    [Authorize]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Gets a user by their ID.
    /// </summary>
    /// <param name="id">The user ID.</param>
    /// <returns>The user with the specified ID.</returns>
    [HttpGet("users/{id}")]
    [Authorize]
    public async Task<IActionResult> GetUserById(string id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="loginDto">The login data transfer object.</param>
    /// <returns>The logged-in user.</returns>
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

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="registerDto">The registration data transfer object.</param>
    /// <returns>The registered user.</returns>
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

    /// <summary>
    /// Deletes a user by their ID.
    /// </summary>
    /// <param name="id">The user ID.</param>
    /// <returns>An action result indicating the outcome of the operation.</returns>
    [HttpDelete("users/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            await _userService.DeleteUser(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}