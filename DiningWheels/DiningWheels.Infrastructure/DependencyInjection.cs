using DiningWheels.Application.Common.Interfaces;
using DiningWheels.Infrastructure.Configuration;
using DiningWheels.Infrastructure.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiningWheels.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpOptions>(configuration.GetSection("Smtp"));
        services.AddTransient<IEmailService, SmtpEmailService>();
        return services;
    }
}