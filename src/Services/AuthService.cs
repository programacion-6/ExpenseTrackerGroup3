using Domain.DTOs;
using Domain.Entities;
using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services.Interfaces;
using ExpenseTrackerGroup3.Utils.Hasher.Interfaces;
using ExpenseTrackerGroup3.Utils.Jwt.Interfaces;

namespace ExpenseTrackerGroup3.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
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

        if(!result)
        {
            throw new ArgumentException("Failed to register user");
        }

        return newUser;
    }

}