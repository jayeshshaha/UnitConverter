using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using UnitConverter.API.DTOs;
using UnitConverter.Domain.Exceptions;

namespace UnitConverter.API.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                // Log expected client errors as warnings, unexpected errors as errors
                if (ex is UnsupportedCategoryException || ex is UnsupportedUnitException || ex is UnsupportedConversionException)
                {
                    logger.LogWarning(ex, "Client error while processing request: {Message}", ex.Message);
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                else
                {
                    logger.LogError(ex, "Unhandled exception while processing request");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

                context.Response.ContentType = "application/json";
                var responsePayload = ApiResponse<object>.Failure(ex.Message);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var jsonResult = JsonSerializer.Serialize(responsePayload, options);
                await context.Response.WriteAsync(jsonResult);
            }
        }
    }
}



