using System.Net;
using System.Text.Json;
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
                if (ex is UnsupportedCategoryException || ex is UnsupportedUnitException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest; 
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

                var responsePayload = new { 
                    message = ex.Message 
                };

                var jsonResult = JsonSerializer.Serialize(responsePayload);
                await context.Response.WriteAsync(jsonResult);
            }
        }
    }
}



