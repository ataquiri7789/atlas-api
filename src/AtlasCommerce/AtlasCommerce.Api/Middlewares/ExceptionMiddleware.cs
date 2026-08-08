using Microsoft.AspNetCore.Mvc;
using Serilog.Context;

namespace AtlasCommerce.Api.Middlewares;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;


    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
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
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception");

            var (status, title) = MapException(exception);

            var correlationId = context.Items["X-Correlation-Id"]?.ToString();

            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            problem.Extensions["traceId"] = correlationId;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;

            await context.Response.WriteAsJsonAsync(problem);
        }
    }

    private static (int status, string title) MapException(Exception ex)
    {
        return ex switch
        {
            UnauthorizedAccessException =>
                (StatusCodes.Status401Unauthorized, "Unauthorized"),

            KeyNotFoundException =>
                (StatusCodes.Status404NotFound, "Resource not found"),

            ArgumentException =>
                (StatusCodes.Status400BadRequest, "Bad request"),

            TimeoutException =>
                (StatusCodes.Status504GatewayTimeout, "Timeout"),

            HttpRequestException =>
                (StatusCodes.Status503ServiceUnavailable, "External service error"),

            _ =>
                (StatusCodes.Status500InternalServerError, "Internal Server Error")
        };
    }
}