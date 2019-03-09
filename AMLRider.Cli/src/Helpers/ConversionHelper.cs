using System;

namespace AMLRider.Cli.Helpers
{
    
    /// <summary>
    /// Helper functions for managing type conversions.
    /// </summary>
    public class ConversionHelper
    {

        /// <summary>
        /// Checks whether an object can be converted to the specified type
        /// when <see cref="Convert.ChangeType(object, System.Type)"/> is used.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="conversionType">The type to convert to.</param>
        /// <returns>True, if the type is convertible.</returns>
        public static bool CanChangeType(object value, Type conversionType)
        {
            if (value == null)
                return false;

            if (conversionType == null)
                return false;

            return value is IConvertible;
        }

    }
    
}