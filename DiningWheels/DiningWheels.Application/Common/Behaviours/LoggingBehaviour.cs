using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace DiningWheels.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var RequestName = typeof(TRequest).Name;
        
        _logger.LogInformation("Dining Wheels Requests: {Name} {@Request}", RequestName, request);
    }
}