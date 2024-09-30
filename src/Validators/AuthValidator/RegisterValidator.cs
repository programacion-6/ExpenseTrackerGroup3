using Domain.DTOs;
using ExpenseTrackerGroup3.Validators.Rules;
using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.AuthValidator;

public class RegisterValidator : AbstractValidator<CreateUser>
{
    public RegisterValidator()
    {   
        RuleFor(user => user.Name).Username();
        RuleFor(user => user.Email).Email();
        RuleFor(user => user.Password).Password();
    }
}
