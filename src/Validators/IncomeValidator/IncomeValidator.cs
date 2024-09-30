using Domain.DTOs;
using ExpenseTrackerGroup3.Validators.Rules;
using FluentValidation;

namespace ExpenseTrackerGroup3.Validators.IncomeValidator;

public class IncomeValidator : AbstractValidator<CreateIncome>
{
    public IncomeValidator()
    {   
        RuleFor(Income => Income.Amount).Amount();
        RuleFor(Income => Income.Source).Source();
    }
}
