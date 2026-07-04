using UnitConverter.Domain.Enums;

namespace UnitConverter.Services.Interfaces
{
    public interface IConversionStrategy
    {
        public ConversionCategory Category { get;}
        public double Convert(string fromUnit, string toUnit, double value);
    }
}
