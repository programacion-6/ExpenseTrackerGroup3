using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Domain.DTOs;

namespace ExpenseTrackerGroup3.Services.Interfaces;

public interface IAuthService
{
    Task<User> RegisterUserAsync(CreateUser user);
    Task<LoginResponse> LoginUserAsync(string email, string password);
    Task RequestResetPasswordAsync(RequestResetPassword request);
    Task ResetPasswordAsync(ResetPassword resetPassword);
}
