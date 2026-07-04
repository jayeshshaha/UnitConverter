using UnitConverter.Domain.ConversionRules;
using UnitConverter.Domain.Exceptions;
namespace UnitConverter.Domain.Converters
{
    public static class TemperatureConverter
    {
        public static double Convert(string fromUnit, string toUnit, double value)
        {
            var key = (fromUnit.ToLowerInvariant(), toUnit.ToLowerInvariant());
            if(!TemperatureConversionRules.Conversions.TryGetValue(key, out var conversion))
            {
                throw new UnsupportedConversionException(fromUnit, toUnit);
            }

            return conversion(value);
        }
    }
}

