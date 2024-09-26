using Domain.Entities;

namespace Domain.DTOs;

public record ResponseGoal
(
  Guid Id,
  Guid UserId,
  decimal GoalAmount,
  DateTime DeadLine,
  decimal CurrentAmount,
  DateTime CreatedAt
)
{
  public static ResponseGoal FromDomain(Goal goal)
  {
    return new ResponseGoal
    (
      goal.Id,
      goal.UserId,
      goal.GoalAmount,
      goal.DeadLine,
      goal.CurrentAmount,
      goal.CreatedAt
    );
  }
}
