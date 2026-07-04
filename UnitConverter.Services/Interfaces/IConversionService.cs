using UnitConverter.Domain.Enums;

namespace UnitConverter.Services.Interfaces
{
    public interface IConversionService
    {
        public double Convert(ConversionCategory category, string fromUnit, string toUnit, double value);
    }
}
