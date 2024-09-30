using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Validators.Rules;
using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.UserValidator;

public class UserValidator : AbstractValidator<UpdateUserDTO>
{
    public UserValidator()
    {
        RuleFor(user => user.Name).Username();
        RuleFor(user => user.Email).Email();
    }
}
