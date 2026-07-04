using UnitConverter.Domain.ConversionRules;

namespace UnitConverter.Domain.Converters
{
    public static class LengthConverter
    {
       
        static readonly IReadOnlyDictionary<string, double> UnitFactors = LengthConversionRules.UnitFactors;

        public static double Convert(string fromUnit, string toUnit, double value)
        {
            if (!UnitFactors.TryGetValue(fromUnit, out var metersPerFromUnit))
            {
                throw new NotSupportedException($"Unsupported source unit: {fromUnit}");
            }

            if (!UnitFactors.TryGetValue(toUnit, out var metersPerToUnit))
            {
                throw new NotSupportedException($"Unsupported target unit: {toUnit}");
            }

            double valueInMeters = value * metersPerFromUnit;

            double finalValue = valueInMeters / metersPerToUnit;

            return finalValue;
        }
    }
}
