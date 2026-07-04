namespace UnitConverter.API.DTOs
{
    public class ConversionResponse
    {
        public double OriginalValue { get; set; }
        public required string FromUnit { get; set; }
        public required string ToUnit { get; set; }
        public double ConvertedValue { get; set; }
    }
}
