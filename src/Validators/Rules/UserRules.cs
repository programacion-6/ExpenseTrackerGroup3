using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.Rules;

public static class UserRules
{
    public static IRuleBuilderOptions<T, string> Email<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("The email must be in a valid format.");
    }

    public static IRuleBuilderOptions<T, string> Username<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
                .NotEmpty()
                .WithMessage("Name is required.")
                .MinimumLength(3)
                .WithMessage("The name must be at least 3 characters.")
                .MaximumLength(50)
                .WithMessage("The name should not be longer than 50 characters.")
                .Matches(@"^[a-zA-Z\s]+$")
                .WithMessage("The name can only contain letters and spaces.");
    }

    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("The password must be at least 8 characters.")
            .MaximumLength(50)
            .WithMessage("The password should not be longer than 50 characters.")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("The password must contain at least one special character (e.g., @, #, $, etc.).")
            .Matches("[A-Z]")
            .WithMessage("The password must contain at least one uppercase letter.")
            .Matches("[a-z]")
            .WithMessage("The password must contain at least one lowercase letter.")
            .Matches("[0-9]")
            .WithMessage("The password must contain at least one number.")
            .Must(password => !password.Contains(" "))
            .WithMessage("Password cannot contain spaces.");
    }
}
