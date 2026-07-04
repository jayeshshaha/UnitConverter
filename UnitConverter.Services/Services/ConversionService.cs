using UnitConverter.Domain.Enums;
using UnitConverter.Domain.Exceptions;
using UnitConverter.Services.Interfaces;

namespace UnitConverter.Services.Services
{
    public class ConversionService(IEnumerable<IConversionStrategy> strategies) : IConversionService
    {

        public double Convert(ConversionCategory category, string fromUnit, string toUnit, double value)
        {
            var strategy = strategies.FirstOrDefault(s => s.Category == category);
            if (strategy == null)
            {
                throw new UnsupportedCategoryException(category.ToString());
            }

            return strategy.Convert(fromUnit, toUnit, value);
        }
    }
}
            
    


