namespace DiningWheels.Infrastructure.Configuration;

public class SmtpOptions
{
    public required string Host { get; set; }
    public int Port { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string From { get; set; }
    public bool EnableSsl { get; set; } = true;
}