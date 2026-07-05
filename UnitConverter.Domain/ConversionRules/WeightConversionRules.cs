namespace UnitConverter.Domain.ConversionRules
{
    public static class WeightConversionRules
    {
        public static readonly Dictionary<string, double> UnitFactors = new(StringComparer.OrdinalIgnoreCase) {
            ["kilogram"] = 1.0,
            ["gram"] = 0.001
        };

    }
}
