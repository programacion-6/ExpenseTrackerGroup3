using Domain.Entities;

namespace Domain.DTOs;

public record UpdateIncome
(
  Guid Id,
  decimal? Amount,
  string? Source,
  DateTime? CreatedAt
)
{
  public Income ToDomain(Income income)
  {
    income.Amount = Amount ?? income.Amount;
    income.Source = Source ?? income.Source;
    income.CreatedAt = CreatedAt ?? income.CreatedAt;

    return income;
  }
}