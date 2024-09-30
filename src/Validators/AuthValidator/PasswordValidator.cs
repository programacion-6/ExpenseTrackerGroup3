using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Validators.Rules;
using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.AuthValidator;

public class PasswordValidator : AbstractValidator<ResetPassword>
{
    public PasswordValidator()
    {
        RuleFor(user => user.Password).Password();
    }
}
