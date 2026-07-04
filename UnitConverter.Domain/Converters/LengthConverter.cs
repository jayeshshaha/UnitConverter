using UnitConverter.Domain.ConversionRules;
using UnitConverter.Domain.Exceptions;

namespace UnitConverter.Domain.Converters
{
    public static class LengthConverter
    {
       
        static readonly IReadOnlyDictionary<string, double> UnitFactors = LengthConversionRules.UnitFactors;

        public static double Convert(string fromUnit, string toUnit, double value)
        {
            if (!UnitFactors.TryGetValue(fromUnit, out var metersPerFromUnit))
            {
                throw new UnsupportedUnitException(fromUnit);
            }

            if (!UnitFactors.TryGetValue(toUnit, out var metersPerToUnit))
            {
                throw new UnsupportedUnitException(toUnit);
            }

            double valueInMeters = value * metersPerFromUnit;

            double finalValue = valueInMeters / metersPerToUnit;

            return finalValue;
        }
    }
}
