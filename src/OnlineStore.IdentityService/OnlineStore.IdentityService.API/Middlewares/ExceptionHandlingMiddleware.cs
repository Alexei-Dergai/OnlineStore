using FluentValidation;
using OnlineStore.IdentityService.API.Models;
using OnlineStore.IdentityService.BLL.Exceptions;
using System.Net;
using System.Text.Json;

namespace OnlineStore.IdentityService.API.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var statusCode = ex switch
                {
                    ValidationException => HttpStatusCode.BadRequest,
                    EntityAlreadyExistsException => HttpStatusCode.Conflict,
                    UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                    _ => HttpStatusCode.InternalServerError
                };

                var message = ex switch
                {
                    ValidationException => GetValidationExceptionMessage((ValidationException)ex),
                    _ => ex.Message
                };

                var apiResponse = new ApiResponse
                {
                    StatusCode = (int)statusCode,
                    Message = message
                };

                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse));
            }
        }

        private string GetValidationExceptionMessage(ValidationException ex)
        {
            var errors = ex.Errors.Select(x => x.ErrorMessage);
            var message = string.Join(Environment.NewLine, errors);

            return message;
        }
    }
}
