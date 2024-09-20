using Domain.Entities;

namespace Domain.DTOs;

public record CreateBudget
(
  DateTime Month,
  decimal BudgetAmount,
  decimal AlertThreshold
)
{
  public Budget ToDomain()
  {
    return new Budget
    {
      Month = Month,
      BudgetAmount = BudgetAmount,
      AlertThreshold = AlertThreshold
    };
  }
}