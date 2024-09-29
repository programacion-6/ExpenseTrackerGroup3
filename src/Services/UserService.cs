
using Domain.Entities;

using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Exceptions;
using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services.Interfaces;
using ExpenseTrackerGroup3.Utils.Exception;
using FluentValidation;

namespace ExpenseTrackerGroup3.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UpdateUserDTO> _userValidator;

    public UserService(
        IUserRepository userRepository,
        IValidator<UpdateUserDTO> userValidator)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
    }

    public async Task<User?> GetUserProfileAsync(Guid userId)
    {
        var userExists = await _userRepository.GetByIdAsync(userId);
        userExists.ThrowIfNull("User not found");
        return userExists;
    }

    public async Task<User> UpdateUserProfileAsync(Guid userId, UpdateUserDTO user)
    {
        var validateResult = await _userValidator.ValidateAsync(user);
        validateResult.ThrowIfValidationFailed();

        var userExists = await _userRepository.GetByIdAsync(userId);
        userExists.ThrowIfNull("User not found");

        var updatedUser = user.ToDomain(userExists!);
        var success = await  _userRepository.UpdateAsync(updatedUser);
        success.ThrowIfOperationFailed("Failed to update user");

        return updatedUser;
    }
}
