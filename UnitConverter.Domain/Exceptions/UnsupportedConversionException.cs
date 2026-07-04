using System;
using System.Collections.Generic;
using System.Text;

namespace UnitConverter.Domain.Exceptions
{
    public class UnsupportedConversionException : Exception
    {
        public UnsupportedConversionException(string fromUnit, string toUnit) : base( $"Conversion from '{fromUnit}'to'{toUnit}' is not supported.")
        {

        }
    }
}
