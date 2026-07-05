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


            services.AddScoped<IConversionService, ConversionService>();
            services.AddScoped<IConversionStrategy, LengthConversionStrategy>();
            services.AddScoped<IConversionStrategy, TemperatureConversionStrategy>();
            services.AddScoped<IConversionStrategy, WeightConversionStrategy>();

            return services;
        }
    }
}
