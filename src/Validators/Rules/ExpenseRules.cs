using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.Rules;

public static class ExpenseRules
{
    public static IRuleBuilderOptions<T, string?> Description<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(description => string.IsNullOrEmpty(description) || description.Length <= 255)
            .WithMessage("Description must be null or have a maximum length of 255 characters.")
            .MaximumLength(255)
            .WithMessage("Description cannot be longer than 255 characters.");
    }

    public static IRuleBuilderOptions<T, string> Category<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Category is required.")
            .MaximumLength(50)
            .WithMessage("Category cannot be longer than 50 characters.");
    }

    public static IRuleBuilderOptions<T, DateTime> Date<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Date is required.")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("The date cannot be in the future.");
    }

     public static IRuleBuilderOptions<T, Boolean> RecurringExpense<T>(this IRuleBuilder<T, Boolean> ruleBuilder)
    {
        return ruleBuilder
            .Must(beBoolean => beBoolean == true || beBoolean == false)
            .WithMessage("RecurringExpense must be a boolean value.");
    }   
}
