using Microsoft.AspNetCore.Diagnostics;
using mlsaDemo.Models;
using System.Net;

namespace mlsaDemo.Middleware
{

    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // Log the error message.
            _logger.LogError(
                $"An error occurred while processing your request: {exception.Message}");

            // Create an ErrorResponse object to send back to the client.
            var errorResponse = new ErrorResponse
            {
                Message = exception.Message
            };

            // Determine the status code and title based on the type of exception.
            switch (exception)
            {
                case BadHttpRequestException:
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Title = exception.GetType().Name;
                    break;

                default:
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Title = "Internal Server Error";
                    break;
            }

            // Set the status code on the HTTP response.
            httpContext.Response.StatusCode = errorResponse.StatusCode;

            // Write the ErrorResponse object to the HTTP response as JSON.
            await httpContext
                .Response
                .WriteAsJsonAsync(errorResponse, cancellationToken);

            // Return true to indicate that the exception was handled.
            return true;
        }
    }
}
