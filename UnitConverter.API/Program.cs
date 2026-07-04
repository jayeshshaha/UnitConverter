
using UnitConverter.API.Extensions;
using UnitConverter.API.Middleware;
using UnitConverter.Services.Interfaces;
using UnitConverter.Services.Services;
using UnitConverter.Services.Strategies;

namespace UnitConverter.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSwaggerExtension();

            builder.Services.AddControllers();

            builder.Services.AddScoped<IConversionStrategy, LengthConversionStrategy>();

            builder.Services.AddScoped<IConversionService, ConversionService>();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

   
            app.MapControllers();

            app.Run();
        }
    }
}
