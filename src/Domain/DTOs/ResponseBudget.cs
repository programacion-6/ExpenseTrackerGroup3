using Domain.Entities;

namespace Domain.DTOs;

public record ResponseBudget
(
  Guid Id,
  Guid UserId,
  DateTime Month,
  decimal BudgetAmount,
  decimal AlertThreshold
)
{
  public static ResponseBudget FromDomain(Budget budget)
  {
    return new ResponseBudget
    (
      budget.Id,
      budget.UserId,
      budget.Month,
      budget.BudgetAmount,
      budget.AlertThreshold
    );
  }
}