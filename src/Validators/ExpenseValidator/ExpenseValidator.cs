using Domain.DTOs;
using ExpenseTrackerGroup3.Validators.Rules;
using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.ExpenseValidator;

public class ExpenseValidator : AbstractValidator<CreateExpense>
{
    public ExpenseValidator()
    {   
        RuleFor(Expense => Expense.Amount).Amount();
        RuleFor(Expense => Expense.Description).Description();
        RuleFor(Expense => Expense.Category).Category();
        RuleFor(Expense => Expense.Date).Date();
        RuleFor(Expense => Expense.RecurringExpense).RecurringExpense();
    }
}
