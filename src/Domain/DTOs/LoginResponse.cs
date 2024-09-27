namespace ExpenseTrackerGroup3.Domain.DTOs;

public record LoginResponse
(
    string Email,
    string Token
)
{
    public static LoginResponse FromDomain(LoginResponse login)
    {
        return new LoginResponse
        (
            login.Email,
            login.Token
        );
    }
}
