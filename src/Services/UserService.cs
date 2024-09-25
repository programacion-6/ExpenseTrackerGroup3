
using Domain.Entities;

using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services.Interfaces;

namespace ExpenseTrackerGroup3.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User?> GetUserProfileAsync(Guid userId)
    {
        var userExists = _userRepository.GetByIdAsync(userId);

        if (userExists == null)
        {
            throw new ArgumentException("User not found");
        }

        return await userExists;
    }

    public async Task<User> UpdateUserProfileAsync(Guid userId, UpdateUserDTO user)
    {
        var userExists = await _userRepository.GetByIdAsync(userId);

        if (userExists == null)
        {
            throw new ArgumentException("User not found");
        }

        var updatedUser = user.ToDomain(userExists);
        var success = await  _userRepository.UpdateAsync(updatedUser);
        
        if (!success)
        {
            throw new Exception("Failed to update user");
        }

        return updatedUser;
    }
}
