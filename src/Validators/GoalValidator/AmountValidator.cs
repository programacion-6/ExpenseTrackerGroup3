using Domain.Entities;
using ExpenseTrackerGroup3.Validators.Rules;
using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.GoalValidator;

public class AmountValidator : AbstractValidator<Goal>
{
    public AmountValidator()
    {   
        RuleFor(Goal => Goal.GoalAmount).Amount();
    }
}
