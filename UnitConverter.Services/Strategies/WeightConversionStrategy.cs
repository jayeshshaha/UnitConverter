using UnitConverter.Domain.Converters;
using UnitConverter.Domain.Enums;
using UnitConverter.Services.Interfaces;

namespace UnitConverter.Services.Strategies
{
    public class WeightConversionStrategy : IConversionStrategy
    {
        public ConversionCategory Category => ConversionCategory.Weight;

        public double Convert(string fromUnit, string toUnit, double value)
        {
            return WeightConverter.Convert(fromUnit, toUnit, value);
        }
    }
}
