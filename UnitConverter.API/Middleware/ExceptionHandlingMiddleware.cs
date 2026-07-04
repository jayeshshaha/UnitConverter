using System.Net;
using System.Text.Json;
using UnitConverter.API.DTOs;
using UnitConverter.Domain.Exceptions;

namespace UnitConverter.API.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                if (ex is UnsupportedCategoryException || ex is UnsupportedUnitException || ex is UnsupportedConversionException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest; 
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

                var responsePayload = ApiResponse<object>.Failure(ex.Message);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var jsonResult = JsonSerializer.Serialize(responsePayload, options);
                await context.Response.WriteAsync(jsonResult);
            }
        }
    }
}



