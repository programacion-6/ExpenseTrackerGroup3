using ExpenseTrackerGroup3.Utils.NotifyMilestone.Interfaces;

namespace ExpenseTrackerGroup3.Utils.NotifyMilestone;

public class GoalNotificationStrategy : IGoalNotificationStrategy
{
    private readonly decimal _threshold;
    private readonly string _subject;
    private readonly string _bodyTemplate;

    public GoalNotificationStrategy(decimal threshold, string subject, string bodyTemplate)
    {
        _threshold = threshold;
        _subject = subject;
        _bodyTemplate = bodyTemplate;
    }

    public bool ShouldNotify(decimal goalProgress, DateTime deadline)
    {
        return goalProgress >= _threshold && deadline > DateTime.Now;
    }

    public string GetSubject()
    {
        return _subject;
    }

    public string GetBody(string userName)
    {
        return string.Format(_bodyTemplate, userName);
    }
}