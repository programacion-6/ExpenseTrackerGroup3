public class SmtpOptions
{
    public string? Host { get; set; } = Environment.GetEnvironmentVariable("SMTP_HOST");
    public int Port { get; set; } = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587");
    public string? Username { get; set; } = Environment.GetEnvironmentVariable("SMTP_USERNAME");
    public string? Password { get; set; } = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
    public string? FromEmail { get; set; } = Environment.GetEnvironmentVariable("SMTP_FROM_EMAIL");
    public bool EnableSsl { get; set; } = bool.Parse(Environment.GetEnvironmentVariable("SMTP_ENABLE_SSL") ?? "true");
}
