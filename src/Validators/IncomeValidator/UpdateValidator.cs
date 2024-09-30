using Domain.DTOs;
using ExpenseTrackerGroup3.Validators.Rules;
using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.IncomeValidator;

public class UpdateValidator : AbstractValidator<UpdateIncome>
{
    public UpdateValidator()
    {
        RuleFor(Income => Income.Amount)
            .Must(amount => amount == null || amount > 0)
            .WithMessage("Amount must be greater than 0 or null.")
            .DependentRules(() =>
            {
                RuleFor(income => income.Amount.Value).Amount();
            });
        RuleFor(Income => Income.Source).Source();
    }
}
