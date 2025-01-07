using MediatR;
using Microsoft.Extensions.Logging;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;

namespace UnclewoodCleanArchitecture.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var name = request.GetType().Name;
        try
        {
            _logger.LogInformation("Executing command {command}", name);
            var result = await next();
            _logger.LogInformation("Command {command} passed successfully", name);
            return result;
        }
        catch (Exception e)
        {
           _logger.LogError(e, "Command {command} processing failed", name);
            throw;
        }
    }
}