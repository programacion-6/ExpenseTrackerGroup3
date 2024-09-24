
using Domain.Entities;

using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Services.Interfaces;

namespace ExpenseTrackerGroup3.Services;

public class UserService : IUserService
{
    public Task<User> GetUserProfileAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUserProfileAsync(Guid userId, UpdateUserDTO user)
    {
        throw new NotImplementedException();
    }
}
