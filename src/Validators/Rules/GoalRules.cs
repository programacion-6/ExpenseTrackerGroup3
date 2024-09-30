using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.Rules;

public static class GoalRules
{
    public static IRuleBuilderOptions<T, DateTime> DeadLine<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThan(DateTime.Now)
            .WithMessage("The deadline must be in the future.");
    }

    public static IRuleBuilderOptions<T, decimal> CurrentAmount<T>(this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThanOrEqualTo(0)
            .WithMessage("Current amount must be greater than or equal to 0.");
    }
}
