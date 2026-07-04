using Microsoft.OpenApi;

namespace UnitConverter.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerExtension(this IServiceCollection services)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Unit Converter API",
                    Version = "v1",
                    Description = "A RESTful API for converting units."
                });
            });

            return services;
        }
    }
}
