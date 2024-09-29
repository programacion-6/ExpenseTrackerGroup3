namespace ExpenseTrackerGroup3.Utils.NotifyMilestone.Interfaces;

public interface IGoalNotificationStrategy
{
    bool ShouldNotify(decimal goalProgress, DateTime deadline);
    string GetSubject();
    string GetBody(string userName);
}