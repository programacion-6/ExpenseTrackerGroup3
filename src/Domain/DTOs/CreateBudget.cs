using Domain.Entities;

namespace Domain.DTOs;

public record CreateBudget
(
  decimal BudgetAmount,
  decimal AlertThreshold
)
{
  public Budget ToDomain()
  {
    return new Budget
    {
      BudgetAmount = BudgetAmount,
      AlertThreshold = AlertThreshold
    };
  }
}