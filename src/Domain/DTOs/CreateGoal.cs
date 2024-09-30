using Domain.Entities;

namespace Domain.DTOs;

public record CreateGoal
(
  decimal GoalAmount,
  DateTime DeadLine,
  decimal CurrentAmount
)
{
  public Goal ToDomain()
  {
    return new Goal
    {
      GoalAmount = GoalAmount,
      DeadLine = DeadLine,
      CurrentAmount = CurrentAmount,
      CreatedAt = DateTime.Now
    };
  }
}
