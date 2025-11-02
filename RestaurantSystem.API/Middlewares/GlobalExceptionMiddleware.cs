using System.Net;
using System.Text.Json;

namespace RestaurantSystem.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
            private readonly RequestDelegate _next;
            private readonly ILogger<GlobalExceptionMiddleware> _logger;

            public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
                    _logger.LogError(ex, "Unhandled exception occurred");

                    await HandleExceptionAsync(context, ex);
                }
            }

            private Task HandleExceptionAsync(HttpContext context, Exception exception)
            {
                HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
                string message = exception.Message;

                if (exception is ArgumentException)
                    statusCode = HttpStatusCode.BadRequest;
                else if (exception is UnauthorizedAccessException)
                    statusCode = HttpStatusCode.Unauthorized;

                var result = JsonSerializer.Serialize(new
                {
                    statusCode = (int)statusCode,
                    message = message
                });

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;

                return context.Response.WriteAsync(result);
            }
        }
    }

