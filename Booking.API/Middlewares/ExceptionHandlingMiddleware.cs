
using System.Net.Mime;
using System.Text.Json;

namespace Booking.API.Middlewares;
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, exception.Message);
        context.Response.ContentType = MediaTypeNames.Application.Json;

        ErrorResponse? ResponseModel = default;

        if (exception is NotFoundException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            ResponseModel = new()
            {
                Status = context.Response.StatusCode,
                Error = exception.Message,
                Instance = $"{context.Request.Method} {context.Request.Path}",
                Title = "Not Found",
                Type = context.TraceIdentifier,
                Detail = "The specified resource was not found"
            };

        }

        var Options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var Json = JsonSerializer.Serialize(ResponseModel, Options);
        await context.Response.WriteAsync(Json);

        return;
    }
}
