using System.Net;
using System.Net.Mail;

namespace ExpenseTrackerGroup3.Utils.EmailSender;

public class EmailSender : IEmailSender
{
    private readonly SmtpOptions _smtpOptions;

    public EmailSender(SmtpOptions smtpOptions)
    {
        _smtpOptions = smtpOptions;
    }

    public async Task SendEmail(string to, string subject, string body)
    {
        using (var client = new SmtpClient(_smtpOptions.Host, _smtpOptions.Port))
        {
            client.Credentials = new NetworkCredential(_smtpOptions.Username, _smtpOptions.Password);
            client.EnableSsl = _smtpOptions.EnableSsl;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpOptions.FromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }
    }
}
