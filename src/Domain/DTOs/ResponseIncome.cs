using Domain.Entities;

namespace Domain.DTOs;

public record ResponseIncome
(
  Guid Id,
  Guid UserId,
  decimal Amount,
  string Source,
  DateTime CreatedAt
)
{
  public static ResponseIncome FromDomain(Income income)
  {
    return new ResponseIncome
    (
      income.Id,
      income.UserId,
      income.Amount,
      income.Source,
      income.CreatedAt
    );
  }
}