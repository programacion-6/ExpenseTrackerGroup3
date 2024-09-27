namespace ExpenseTrackerGroup3.Utils.EmailSender;

public interface IEmailSender
{
    Task SendEmail(string to, string subject, string body);
}
