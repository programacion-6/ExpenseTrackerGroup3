using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.Rules;

public static class BudgetRules
{
    public static IRuleBuilderOptions<T, decimal> Amount<T>(this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Amount is required")
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0.")
            .Must(amount => amount.ToString("F2").Length <= 10)
            .WithMessage("Amount must have up to 10 total digits and 2 decimal places.");
    }

    public static IRuleBuilderOptions<T, decimal> AlertThreshold<T>(this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThanOrEqualTo(1)
            .WithMessage("Alert threshold must be greater than or equal to 1.")
            .LessThanOrEqualTo(100)
            .WithMessage("Alert threshold must be less than or equal to 100.");
    }
}
