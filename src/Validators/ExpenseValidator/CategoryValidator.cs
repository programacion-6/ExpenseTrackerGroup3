using Domain.Entities;
using ExpenseTrackerGroup3.Validators.Rules;
using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.ExpenseValidator;

public class CategoryValidator : AbstractValidator<Expense>
{
    public CategoryValidator()
    {   
        RuleFor(Expense => Expense.Amount).Amount();
        RuleFor(Expense => Expense.Category).Category();
    }
}
