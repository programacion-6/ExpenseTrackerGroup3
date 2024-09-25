using Domain.Entities;

using ExpenseTrackerGroup3.Domain.DTOs;

namespace ExpenseTrackerGroup3.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetUserProfileAsync(Guid userId);
    Task<User> UpdateUserProfileAsync(Guid userId, UpdateUserDTO user);
}
