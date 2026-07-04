
using UnitConverter.Domain.Converters;
using UnitConverter.Domain.Enums;
using UnitConverter.Services.Interfaces;

namespace UnitConverter.Services.Strategies
{
    public class LengthConversionStrategy : IConversionStrategy
    {
        public ConversionCategory Category => ConversionCategory.Length;

        public double Convert(string fromUnit, string toUnit, double value)
        {
            return LengthConverter.Convert(fromUnit, toUnit, value);
        }
    }
}
