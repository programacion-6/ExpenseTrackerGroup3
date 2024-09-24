using Domain.Entities;

namespace ExpenseTrackerGroup3.Domain.DTOs;

public record ResponseUserUpdated
(
    Guid Id,
    string Name,
    string Email
)
{
    public static ResponseUserUpdated FromDomain(User user)
    {
        return new ResponseUserUpdated
        (
            user.Id,
            user.Name,
            user.Email
        );
    }
}
