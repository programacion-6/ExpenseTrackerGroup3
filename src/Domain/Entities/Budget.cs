using System;

namespace Domain.Entities;

public class Budget
{
  public required Guid Id { get; set; }
  public required Guid UserId { get; set; }
  public required DateTime Month { get; set; }
  public required decimal BudgetAmount { get; set; }
  public required decimal AlertThreshold { get; set; }
}
