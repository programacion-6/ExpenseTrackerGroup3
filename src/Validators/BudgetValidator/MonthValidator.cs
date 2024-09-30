using Domain.Entities;
using ExpenseTrackerGroup3.Validators.Rules;
using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.BudgetValidator;

public class MonthValidator : AbstractValidator<Budget>
{
    public MonthValidator()
    {
        RuleFor(budget => budget.Month).Date();
    }
}
