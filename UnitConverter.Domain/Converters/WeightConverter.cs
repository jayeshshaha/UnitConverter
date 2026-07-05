using UnitConverter.Domain.ConversionRules;
using UnitConverter.Domain.Exceptions;

namespace UnitConverter.Domain.Converters
{
    public static class WeightConverter
    {
        public static Dictionary<string, double> UnitFactors = WeightConversionRules.UnitFactors;
        public static double Convert(string fromUnit, string toUnit, double value)
        {
            if (!UnitFactors.TryGetValue(fromUnit, out var kgPerFromUnit))
            {
                throw new UnsupportedUnitException(fromUnit);
            }

            if (!UnitFactors.TryGetValue(toUnit, out var kgPerToUnit))
            {
                throw new UnsupportedUnitException(toUnit);
            }

            double valueInKg = value * kgPerFromUnit;

            return valueInKg / kgPerToUnit;
         
        }
    }
}
