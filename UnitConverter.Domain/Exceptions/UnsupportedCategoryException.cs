namespace UnitConverter.Domain.Exceptions
{
    public class UnsupportedCategoryException : Exception
    {
        public UnsupportedCategoryException(string categoryName)
            : base($"The measurement category '{categoryName}' is not supported.")
        {
        }
    }
}
