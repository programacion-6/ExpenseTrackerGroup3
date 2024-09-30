using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Validators.Rules;
using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.AuthValidator;

public class EmailValidator : AbstractValidator<RequestResetPassword>
{
    public EmailValidator()
    {
        RuleFor(user => user.Email).Email();
    }
}
