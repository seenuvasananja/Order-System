using ProductService.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace ProductService.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
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
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = exception switch
            {
                ArgumentNullException => HttpStatusCode.BadRequest,
                ArgumentException => HttpStatusCode.BadRequest,
                KeyNotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };

            response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new
            {
                success = false,
                error = new
                {
                    code = GetErrorCode(exception),
                    message = exception.Message
                }
            });

            return response.WriteAsync(result);
        }

        private static string GetErrorCode(Exception exception)
        {
            return exception switch
            {
                NotFoundException => "NOT_FOUND",
                BadRequestException => "BAD_REQUEST",
                ArgumentException => "INVALID_INPUT",
                _ => "SERVER_ERROR"
            };
        }
    }
}