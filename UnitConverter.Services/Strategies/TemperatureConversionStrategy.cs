using System;
using System.Collections.Generic;
using System.Text;
using UnitConverter.Domain.Converters;
using UnitConverter.Domain.Enums;
using UnitConverter.Services.Interfaces;

namespace UnitConverter.Services.Strategies
{
    public class TemperatureConversionStrategy : IConversionStrategy
    {
        public ConversionCategory Category => ConversionCategory.Temperature;

        public double Convert(string fromUnit, string toUnit, double value)
        {
           return TemperatureConverter.Convert(fromUnit, toUnit, value);
        }
    }
}
