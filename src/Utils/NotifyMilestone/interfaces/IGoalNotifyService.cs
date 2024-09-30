using Domain.Entities;

namespace ExpenseTrackerGroup3.Utils.NotifyMilestone.Interfaces;

public interface IGoalNotifyService
{
    public Task NotifyUserOnMilestoneAsync(User user, decimal goalProgress, Goal goal);
}