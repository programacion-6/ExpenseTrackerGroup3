using Domain.DTOs;

using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGroup3.Controllers;

public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId}/profile")]
    public async Task<ActionResult> GetUserProfile(Guid userId)
    {
        try {
            var user = await _userService.GetUserProfileAsync(userId);
            var response = ResponseUser.FromDomain(user);
            return Ok(response);
        } 
        catch (Exception e) 
        {
            return HandleException(e);
        }
    }

    [HttpPut("{userId}/profile")]
    public async Task<ActionResult> UpdateUserProfile(Guid userId, [FromBody] UpdateUserDTO user)
    {
        try
        {
            var updatedUser = await _userService.UpdateUserProfileAsync(userId, user);
            var response = ResponseUser.FromDomain(updatedUser);
            return Ok(response);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
}
