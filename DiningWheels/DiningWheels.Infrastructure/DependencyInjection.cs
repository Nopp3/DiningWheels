using DiningWheels.Application.Common.Interfaces;
using DiningWheels.Infrastructure.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiningWheels.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IEmailService>(_ => new SmtpEmailService(
            configuration["Smtp:Host"]!,
            int.Parse(configuration["Smtp:Port"]!),
            configuration["Smtp:Username"]!,
            configuration["Smtp:Password"]!,
            configuration["Smtp:From"]!
        ));
        return services;
    }
}