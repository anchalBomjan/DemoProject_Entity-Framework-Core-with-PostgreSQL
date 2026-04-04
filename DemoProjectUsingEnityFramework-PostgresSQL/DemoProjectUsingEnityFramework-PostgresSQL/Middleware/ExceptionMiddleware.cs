using Application.Common.Exceptions;
using Application.Common.Responses;
using System.Net;
using System.Text.Json;

namespace DemoProjectUsingEnityFramework_PostgresSQL.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, response) = exception switch
        {
            AppValidationException validationEx => (
                HttpStatusCode.BadRequest,
                Response<object>.Failure("Validation failed", validationEx.Errors)
            ),
            NotFoundException notFoundEx => (
                HttpStatusCode.NotFound,
                Response<object>.Failure(notFoundEx.Message)
            ),
            KeyNotFoundException keyNotFoundEx => (
                HttpStatusCode.NotFound,
                Response<object>.Failure(keyNotFoundEx.Message)
            ),
            _ => (
                HttpStatusCode.InternalServerError,
                Response<object>.Failure("An unexpected error occurred")
            )
        };

        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}
