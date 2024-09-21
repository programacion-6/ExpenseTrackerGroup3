using System;

namespace Domain.Entities;
public class Goal
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  public decimal GoalAmount { get; set; }
  public DateTime DeadLine { get; set; }
  public decimal CurrentAmount { get; set; }
  public DateTime CreatedAt { get; set; }
}