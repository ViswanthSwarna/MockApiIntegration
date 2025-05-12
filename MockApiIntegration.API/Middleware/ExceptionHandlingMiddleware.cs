using MockApiIntegration.Application.Exceptions;
using System.Text.Json;

namespace MockApiIntegration.API.Middleware
{
    // Middleware to globally catch exceptions and return appropriate HTTP responses.
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException nfEx)
            {
                // Handle custom NotFoundException by returning 404 with error message
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(new { error = nfEx.Message });
            }
            catch (Exception ex)
            {
                // Handle all other unhandled exceptions by returning 500 with message
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new { error = ex.Message });
                await context.Response.WriteAsync(result);
            }
        }
    }

}
