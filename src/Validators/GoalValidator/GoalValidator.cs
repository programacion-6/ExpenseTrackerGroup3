using Domain.DTOs;
using ExpenseTrackerGroup3.Validators.Rules;
using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.GoalValidator;

public class GoalValidator : AbstractValidator<CreateGoal>
{
    public GoalValidator()
    {   
        RuleFor(Goal => Goal.GoalAmount).Amount();
        RuleFor(Goal => Goal.DeadLine).DeadLine();
        RuleFor(Goal => Goal.CurrentAmount).CurrentAmount()
            .LessThanOrEqualTo(Goal => Goal.GoalAmount)
            .WithMessage("Current amount cannot exceed the goal amount.");
    }
}
