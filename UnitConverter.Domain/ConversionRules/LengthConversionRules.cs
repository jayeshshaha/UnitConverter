using System;
using System.Collections.Generic;
using System.Text;

namespace UnitConverter.Domain.ConversionRules
{
    public static class LengthConversionRules
    {
        public static readonly IReadOnlyDictionary<string, double> UnitFactors =
            new Dictionary<string, double>(
                StringComparer.OrdinalIgnoreCase)
            {
                ["meters"] = 1.0,
                ["feet"] = 0.3048
            };
    }
}
