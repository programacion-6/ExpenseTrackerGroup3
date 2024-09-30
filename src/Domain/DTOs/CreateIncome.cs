using Domain.Entities;

namespace Domain.DTOs;

public record CreateIncome
(
  decimal Amount,
  string Source
)
{
  public Income ToDomain()
  {
    return new Income
    {
      Amount = Amount,
      Source = Source
    };
  }
}