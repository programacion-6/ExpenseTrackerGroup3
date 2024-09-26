using Domain.Entities;

namespace Domain.DTOs;

public record UpdateIncome
(
  decimal? Amount,
  string? Source
)
{
  public Income ToDomain(Income income)
  {
    income.Amount = Amount ?? income.Amount;
    income.Source = Source ?? income.Source;

    return income;
  }
}
