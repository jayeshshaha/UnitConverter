namespace UnitConverter.Domain.ConversionRules
{
    public static class TemperatureConversionRules
    {
        public static readonly IReadOnlyDictionary<(string From, string To), Func<double, double>>
            Conversions = new Dictionary<(string From, string To), Func<double, double>>()
            {
                [("celsius", "fahrenheit")] = value => (value * 9 / 5) + 32,
                [("fahrenheit", "celsius")] = value => (value - 32) * 5 / 9,
            };
    }
}
