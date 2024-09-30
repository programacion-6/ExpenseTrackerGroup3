namespace ExpenseTrackerGroup3.Domain.DTOs;

public record ResetPassword
(
    string Password,
    string Token
)
{
    public static ResetPassword FromDomain(ResetPassword reset)
    {
        return new ResetPassword
        (
            reset.Password,
            reset.Token
        );
    }
}