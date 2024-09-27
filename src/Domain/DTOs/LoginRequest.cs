namespace ExpenseTrackerGroup3.Domain.DTOs;

public record LoginRequest
(
    string Email,
    string Password
)
{
    public static LoginRequest FromDomain(LoginRequest login)
    {
        return new LoginRequest
        (
            login.Email,
            login.Password
        );
    }
}
