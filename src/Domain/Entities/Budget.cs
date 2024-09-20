using System;

namespace Domain.Entities;

public class Budget
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public DateTime Month { get; set; }
  public decimal BudgetAmount { get; set; }
  public decimal AlertThreshold { get; set; }
}
