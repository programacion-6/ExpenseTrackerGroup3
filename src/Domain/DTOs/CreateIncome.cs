using Domain.Entities;

namespace Domain.DTOs;

public record CreateIncome
(
  decimal Amount,
  string Source,
  DateTime CreatedAt
)
{
  public Income ToDomain()
  {
    return new Income
    {
      Amount = Amount,
      Source = Source,
      CreatedAt = CreatedAt
    };
  }
}