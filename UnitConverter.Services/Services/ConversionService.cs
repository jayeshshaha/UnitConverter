using Microsoft.Extensions.Logging;
using UnitConverter.Domain.Enums;
using UnitConverter.Domain.Exceptions;
using UnitConverter.Services.Interfaces;

namespace UnitConverter.Services.Services
{
    public class ConversionService(IEnumerable<IConversionStrategy> strategies, ILogger<ConversionService> logger) : IConversionService
    {
     
        public double Convert(ConversionCategory category, string fromUnit, string toUnit, double value)
        {
            logger.LogInformation("Converting {Value} from {FromUnit} to {ToUnit} for category {Category}", value, fromUnit, toUnit, category);

            var strategy = strategies.FirstOrDefault(s => s.Category == category);
            if (strategy == null)
            {
                logger.LogWarning("Unsupported conversion category: {Category}", category);
                throw new UnsupportedCategoryException(category.ToString());
            }

            var result = strategy.Convert(fromUnit, toUnit, value);

            logger.LogInformation("Conversion result: {Result}", result);
            return result;
        }
    }
}
