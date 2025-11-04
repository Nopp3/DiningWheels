namespace DiningWheels.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string message, CancellationToken cancellationToken = default);
}