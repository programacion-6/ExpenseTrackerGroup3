using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services.Interfaces;
using ExpenseTrackerGroup3.Utils.EmailSender;
using ExpenseTrackerGroup3.Utils.Hasher.Interfaces;
using ExpenseTrackerGroup3.Utils.Jwt.Interfaces;

namespace ExpenseTrackerGroup3.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly IEmailSender _emailSender;

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtService jwtService, IEmailSender emailSender)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _emailSender = emailSender;
    }

    public async Task<LoginResponse> LoginUserAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
        {
            throw new ArgumentException("Invalid email or password");
        }

        bool verifiedPassword = _passwordHasher.VerifyPassword(user.PasswordHash, password);

        if (!verifiedPassword)
        {
            throw new ArgumentException("Invalid email or password");
        }

        return new LoginResponse(user.Email, _jwtService.GenerateToken(user.Email, "login", TimeSpan.FromHours(2)));
    }

    public async Task<User> RegisterUserAsync(CreateUser user)
    {
        var passwordHashed = _passwordHasher.HashPassword(user.Password);

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Name = user.Name,
            Email = user.Email,
            PasswordHash = passwordHashed,
            CreatedAt = DateTime.Now
        };

        var result = await _userRepository.CreateAsync(newUser);

        if (!result)
        {
            throw new ArgumentException("Failed to register user");
        }

        return newUser;
    }

    public async Task RequestResetPasswordAsync(RequestResetPassword request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        var resetToken = _jwtService.GenerateToken(user.Email, "resetPassword", TimeSpan.FromHours(1));

        await _emailSender.SendEmail(user.Email, "Reset Your Password", $"Copy the token to  reset your password: {resetToken}");
    }

    public async Task ResetPasswordAsync(ResetPassword reset)
    {
        var email = _jwtService.ValidateToken(reset.Token, "resetPassword");

        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
        {
            throw new ArgumentException("Invalid token or user not found");
        }

        var hashedPassword = _passwordHasher.HashPassword(reset.Password);
        user.PasswordHash = hashedPassword;

        await _userRepository.UpdateAsync(user);
    }

}