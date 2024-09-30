using Domain.Entities;
using ExpenseTrackerGroup3.Validators.Rules;
using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.IncomeValidator;

public class MonthValidator : AbstractValidator<Income>
{
    public MonthValidator()
    {   
        RuleFor(Income => Income.CreatedAt).Date();
    }
}
