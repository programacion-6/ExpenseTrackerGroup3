using Domain.DTOs;
using Domain.Entities;

using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Exceptions;
using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services.Interfaces;
using ExpenseTrackerGroup3.Utils.EmailSender;
using ExpenseTrackerGroup3.Utils.Exception;
using ExpenseTrackerGroup3.Utils.Hasher.Interfaces;
using ExpenseTrackerGroup3.Utils.Jwt.Interfaces;
using FluentValidation;

namespace ExpenseTrackerGroup3.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly IEmailSender _emailSender;
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly IValidator<CreateUser> _createValidator;
    private readonly IValidator<RequestResetPassword> _requestResetValidator;
    private readonly IValidator<ResetPassword> _resetValidator;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService,
        IEmailSender emailSender,
        IValidator<LoginRequest> loginValidator,
        IValidator<CreateUser> createValidator,
        IValidator<RequestResetPassword> requestResetValidator,
        IValidator<ResetPassword> resetValidator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _emailSender = emailSender;
        _loginValidator = loginValidator;
        _createValidator = createValidator;
        _requestResetValidator = requestResetValidator;
        _resetValidator = resetValidator;
    }

    public async Task<LoginResponse> LoginUserAsync(string email, string password)
    {
        var validationResult = await _loginValidator.ValidateAsync( new LoginRequest(email, password) );
        validationResult.ThrowIfValidationFailed();

        var user = await _userRepository.GetByEmailAsync(email);
        user.ThrowIfNull("User not found");

        bool verifiedPassword = _passwordHasher.VerifyPassword(user!.PasswordHash, password);

        if (!verifiedPassword)
        {
            throw new NotFoundException("Password is incorrect");
        }

        return new LoginResponse(user.Email, _jwtService.GenerateToken(user.Id, user.Email, "login", TimeSpan.FromHours(2)));
    }

    public async Task<User> RegisterUserAsync(CreateUser user)
    {
        var validationResult = await _createValidator.ValidateAsync(user);
        validationResult.ThrowIfValidationFailed();

        var existingUser = await _userRepository.GetByEmailAsync(user.Email);
        existingUser.ThrowIfExists("Registration could not be completed. Please check your information and try again.");

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
        result.ThrowIfOperationFailed("Failed to register user");

        return newUser;
    }

    public async Task RequestResetPasswordAsync(RequestResetPassword request)
    {
        var validationResult = await _requestResetValidator.ValidateAsync(request);
        validationResult.ThrowIfValidationFailed();

        var user = await _userRepository.GetByEmailAsync(request.Email);
        user.ThrowIfNull("User not found");

        var resetToken = _jwtService.GenerateToken(user!.Id, user!.Email, "resetPassword", TimeSpan.FromHours(1));

        await _emailSender.SendEmail(user.Email, "Reset Your Password", $"Copy the token to  reset your password: {resetToken}");
    }

    public async Task ResetPasswordAsync(ResetPassword reset)
    {
        var validationResult = await _resetValidator.ValidateAsync(reset);
        validationResult.ThrowIfValidationFailed();

        var email = _jwtService.ValidateToken(reset.Token, "resetPassword");

        var user = await _userRepository.GetByEmailAsync(email);
        user.ThrowIfNull("User not found");

        var hashedPassword = _passwordHasher.HashPassword(reset.Password);
        user!.PasswordHash = hashedPassword;

        await _userRepository.UpdateAsync(user);
    }
}
