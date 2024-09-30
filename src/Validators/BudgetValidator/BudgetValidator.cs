using Domain.DTOs;
using ExpenseTrackerGroup3.Validators.Rules;
using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.BudgetValidator;

public class BudgetValidator : AbstractValidator<CreateBudget>
{
    public BudgetValidator()
    {
        RuleFor(budget => budget.BudgetAmount).Amount();
        RuleFor(budget => budget.AlertThreshold).AlertThreshold();
    }
}
