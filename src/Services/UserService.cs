
using Domain.Entities;

using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Exceptions;
using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services.Interfaces;
using ExpenseTrackerGroup3.Utils.Exception;
using ExpenseTrackerGroup3.Validators.UserValidator;

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
        var userExists = await _userRepository.GetByIdAsync(userId);
        userExists.ThrowIfNull("User not found");
        return userExists;
    }

    public async Task<User> UpdateUserProfileAsync(Guid userId, UpdateUserDTO user)
    {
        var userValidator = new UserValidator();
        var validateResult = await userValidator.ValidateAsync(user);
        validateResult.ThrowIfValidationFailed();

        var userExists = await _userRepository.GetByIdAsync(userId);
        userExists.ThrowIfNull("User not found");

        var updatedUser = user.ToDomain(userExists!);
        var success = await  _userRepository.UpdateAsync(updatedUser);
        success.ThrowIfOperationFailed("Failed to update user");

        return updatedUser;
    }
}
