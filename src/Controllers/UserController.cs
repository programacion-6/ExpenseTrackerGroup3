using Domain.DTOs;
using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGroup3.Controllers;

[Route("api/v1/users")]
public class UserController : ApiControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("profile")]
    public async Task<ActionResult<ResponseUser>> GetUserProfile()
    {
        var userId = GetAuthenticatedUserId();
        var user = await _userService.GetUserProfileAsync(userId);
        return Ok(ResponseUser.FromDomain(user!));
    }

    [HttpPut("profile")]
    public async Task<ActionResult<ResponseUser>> UpdateUserProfile([FromBody] UpdateUserDTO user)
    {
        var userId = GetAuthenticatedUserId();
        var updatedUser = await _userService.UpdateUserProfileAsync(userId, user);
        return Ok(ResponseUser.FromDomain(updatedUser));
    }
}
