using System.Net;
using System.Net.Mail;
using DiningWheels.Application.Common.Interfaces;
using DiningWheels.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace DiningWheels.Infrastructure.Email;

public class SmtpEmailService : IEmailService, IAsyncDisposable
{
    private readonly SmtpClient _client;
    private readonly string _from;

    public SmtpEmailService(IOptions<SmtpOptions> options)
    {
        var o = options.Value;
        _from = o.From;
        _client = new SmtpClient(o.Host, o.Port)
        {
            Credentials = new NetworkCredential(o.Username, o.Password),
            EnableSsl = o.EnableSsl
        };
    }
    
    public async Task SendEmailAsync(string to, string subject, string message, CancellationToken cancellationToken = default)
    {
        using var msg = new MailMessage(_from, to, subject, message);
        await _client.SendMailAsync(msg, cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        _client.Dispose();
        return ValueTask.CompletedTask;
    }
}