using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.Rules;

public static class IncomeRules
{
    public static IRuleBuilderOptions<T, string?> Source<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(x => string.IsNullOrEmpty(x) || x.Length <= 50)
            .WithMessage("Source must be null or have a maximum length of 50 characters.")
            .MaximumLength(50)
            .WithMessage("Source cannot be longer than 50 characters.");
    }
}
