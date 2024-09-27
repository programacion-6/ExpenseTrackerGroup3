namespace ExpenseTrackerGroup3.Domain.DTOs;

public record RequestResetPassword
(
    string Email
)
{
    public static RequestResetPassword FromDomain(RequestResetPassword request)
    {
        return new RequestResetPassword
        (
            request.Email
        );
    }
}
