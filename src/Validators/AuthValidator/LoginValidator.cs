using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Validators.Rules;
using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.AuthValidator;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(user => user.Email).Email();
        RuleFor(user => user.Password).Password();
    }
}
