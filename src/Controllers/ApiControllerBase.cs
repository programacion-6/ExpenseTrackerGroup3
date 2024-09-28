using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGroup3.Controllers;

[Authorize]
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected Guid GetAuthenticatedUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
        {
            throw new UnauthorizedAccessException("Invalid or missing user ID in the token.");
        }   
        return userId;
    }
}
