using System.Text.Json.Serialization;
using UnitConverter.Services.Interfaces;
using UnitConverter.Services.Services;
using UnitConverter.Services.Strategies;

namespace UnitConverter.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                    new JsonStringEnumConverter());
                });


            services.AddScoped<IConversionService, ConversionService>();
            services.AddScoped<IConversionStrategy, LengthConversionStrategy>();
            services.AddScoped<IConversionStrategy, TemperatureConversionStrategy>();

            return services;
        }
    }
}
