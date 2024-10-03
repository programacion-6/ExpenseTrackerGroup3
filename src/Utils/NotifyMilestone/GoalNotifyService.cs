using Domain.Entities;

using ExpenseTrackerGroup3.Utils.EmailSender;
using ExpenseTrackerGroup3.Utils.NotifyMilestone.Interfaces;

namespace ExpenseTrackerGroup3.Utils.NotifyMilestone;

public class GoalNotifyService : IGoalNotifyService
{
    private readonly List<IGoalNotificationStrategy> _strategies;
    private readonly IEmailSender _emailSender;

    public GoalNotifyService(IEmailSender emailSender)
    {
        _emailSender = emailSender;
        _strategies = new List<IGoalNotificationStrategy>
        {
            new GoalNotificationStrategy(100, "Congratulations! Goal 100% achieved", "Dear {0}, congratulations! You've reached 100% of your goal."),
            new GoalNotificationStrategy(90, "Goal Progress: 90% Achieved", "Dear {0}, you're 90% of the way towards your goal. Keep going!"),
            new GoalNotificationStrategy(80, "Goal Progress: 80% Achieved", "Dear {0}, you've reached 80% of your goal. Almost there!"),
            new GoalNotificationStrategy(50, "Goal Progress: 50% Achieved", "Dear {0}, you're halfway through your goal! Great job!")
        };
    }

    public async Task NotifyUserOnMilestoneAsync(User user, decimal goalProgress, Goal goal)
    {
        foreach (var strategy in _strategies)
        {
            if (strategy.ShouldNotify(goalProgress, goal.DeadLine))
            {
                string subject = strategy.GetSubject();
                string body = strategy.GetBody(user.Name);
                await _emailSender.SendEmail(user.Email, subject, body);
                break;
            }
        }
    }
}