namespace ExpenseTrackerGroup3.Utils.Jwt.Interfaces;

public interface IJwtService
{
    public string GenerateToken(Guid userId, string email, string tokenType, TimeSpan tokenExpiration);
    public string ValidateToken(string token, string expectedTokenType);
}
