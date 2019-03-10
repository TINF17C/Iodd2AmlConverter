using System;
using System.Collections.Generic;
using System.Linq;
using AMLRider.Cli.Helpers;

namespace AMLRider.Cli.Core
{
    
    /// <summary>
    /// Responsible for mapping the key value pairs on object properties.
    /// </summary>
    public static class OptionMapper
    {

        /// <summary>
        /// Maps the key value pairs on an objects properties.
        /// </summary>
        /// <param name="optionProperties">The option properties to map the values on.</param>
        /// <param name="pairs">The key value pairs from the command line.</param>
        /// <param name="obj">The target object.</param>
        public static void Map(
            IEnumerable<OptionProperty> optionProperties,
            IEnumerable<KeyValuePair<string, string>> pairs,
            object obj)
        {
            foreach (var pair in pairs)
            {
                var optionProperty = optionProperties
                    .FirstOrDefault(prop => prop.Specification.LongName == pair.Key 
                                            || prop.Specification.ShortName.ToString() == pair.Key);
                
                if (optionProperty == null)
                    continue;

                if (!ConversionHelper.CanChangeType(pair.Value, optionProperty.Property.PropertyType))
                    throw new InvalidOperationException();

                var typedValue = Convert.ChangeType(pair.Value, optionProperty.Property.PropertyType);
                optionProperty.Property.SetValue(obj, typedValue);
            }
        }

    }
    
}