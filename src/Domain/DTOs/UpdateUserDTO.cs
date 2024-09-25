using Domain.Entities;

namespace ExpenseTrackerGroup3.Domain.DTOs;

public record UpdateUserDTO
(
    string Name, 
    string Email 
)
{
    public User ToDomain(User existingUser)
    {
        return new User
        {
            Id = existingUser.Id,
            Name = Name,
            Email = Email,
            PasswordHash = existingUser.PasswordHash,
            CreatedAt = existingUser.CreatedAt
        };
    }
}
