using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using TravelAccommodationBookingPlatform.Domain.Shared.ResultPattern;

namespace TravelAccommodationBookingPlatform.Application.PipelineBehavior;
public sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{

    private readonly ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public RequestLoggingPipelineBehavior(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogInformation("Start processing request {@RequestName},{@DateTimeUtc}",
            requestName,
            DateTime.UtcNow);

        TResponse result = await next();

        if (result.IsFailure)
        {
            using (LogContext.PushProperty("Error", result.Error, true))
            {
                _logger.LogError("Request {@RequestName} failed", requestName);
            }
        }

        _logger.LogInformation("Completed request {@RequestName},{@DateTimeUtc}",
            requestName,
            DateTime.UtcNow);

        return result;

    }
}
