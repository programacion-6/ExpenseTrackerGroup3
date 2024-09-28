using Domain.DTOs;

using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGroup3.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(ResponseUser), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> RegisterUser([FromBody] CreateUser user)
    {
        var newUser = await _authService.RegisterUserAsync(user);
        var response = ResponseUser.FromDomain(newUser);
        return Ok(response);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> LoginUser([FromBody] LoginRequest loginRequest)
    {
        var user = await _authService.LoginUserAsync(loginRequest.Email, loginRequest.Password);
        var response = LoginResponse.FromDomain(user);
        return Ok(response);
    }

    [HttpPost("requestResetPassword")]
    [Authorize]
    [ProducesResponseType(typeof(RequestResetPassword), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> RequestResetPassword([FromBody] RequestResetPassword request)
    {
        await _authService.RequestResetPasswordAsync(request);
        return Ok("Reset password email sent, check your inbox.");
    }

    [HttpPut("resetPassword")]
    [Authorize]
    [ProducesResponseType(typeof(ResetPassword), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPassword reset)
    {
        await _authService.ResetPasswordAsync(reset);
        return Ok("Password has been reset successfully.");
    }
}
