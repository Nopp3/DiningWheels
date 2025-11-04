using System.Net;
using System.Net.Mail;
using DiningWheels.Application.Common.Interfaces;

namespace DiningWheels.Infrastructure.Email;

public class SmtpEmailService : IEmailService
{
    private readonly SmtpClient _client;
    private readonly string _from;

    public SmtpEmailService(string host, int port, string username, string password, string from)
    {
        _from = from;
        _client = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(username, password),
            EnableSsl = true
        };
    }
    
    public async Task SendEmailAsync(string to, string subject, string message, CancellationToken cancellationToken = default)
    {
        var msg = new MailMessage(_from, to, subject, message);
        await _client.SendMailAsync(msg, cancellationToken);
    }
}