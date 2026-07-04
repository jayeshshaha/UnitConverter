using System;
using System.Collections.Generic;
using System.Text;

namespace UnitConverter.Domain.Exceptions
{
    public class UnsupportedUnitException : Exception
    {
        public UnsupportedUnitException(string unitName)
            : base($"The unit '{unitName}' is not supported by the system.")
        {
        }

    }
}
