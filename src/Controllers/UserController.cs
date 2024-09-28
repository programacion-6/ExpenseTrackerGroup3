using Domain.DTOs;

using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGroup3.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId}/profile")]
    public async Task<ActionResult<ResponseUser>> GetUserProfile(Guid userId)
    {
        var user = await _userService.GetUserProfileAsync(userId);
        return Ok(ResponseUser.FromDomain(user!));
    }

    [HttpPut("{userId}/profile")]
    public async Task<ActionResult<ResponseUser>> UpdateUserProfile(Guid userId, [FromBody] UpdateUserDTO user)
    {
        var updatedUser = await _userService.UpdateUserProfileAsync(userId, user);
        return Ok(ResponseUser.FromDomain(updatedUser));
    }
}
