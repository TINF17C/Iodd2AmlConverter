using System;
using System.Collections.Generic;
using System.Linq;
using Iodd2AmlConverter.Cli.Helpers;

namespace Iodd2AmlConverter.Cli.Core
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
            if(optionProperties == null)
                throw new ArgumentNullException($"{nameof(optionProperties)} cannot be null.");
            
            if(obj == null)
                throw new ArgumentNullException($"{nameof(obj)} cannot be null.");
            
            var mappedProperties = new List<OptionProperty>();
            foreach (var pair in pairs)
            {
                var optionProperty = optionProperties
                    .FirstOrDefault(prop => prop.Specification.LongName == pair.Key 
                                            || prop.Specification.ShortName.ToString() == pair.Key);
                
                if (optionProperty == null)
                    continue;

                if (!ConversionHelper.CanChangeType(pair.Value, optionProperty.Property.PropertyType))
                    throw new InvalidOperationException();

                var property = optionProperty.Property;
                if(!property.CanWrite)
                    throw new InvalidOperationException($"Not allowed to write property: {property.Name}");
                
                var typedValue = Convert.ChangeType(pair.Value, property.PropertyType);
                
                property.SetValue(obj, typedValue);
                mappedProperties.Add(optionProperty);
            }
            
            if(!AreRequiredOptionsMapped(optionProperties, mappedProperties))
                throw new InvalidOperationException("Not all required options has been set.");
        }

        /// <summary>
        /// Checks whether all required options are set.
        /// </summary>
        /// <param name="optionProperties">The whole list of option properties.</param>
        /// <param name="mappedProperties">The list of all mapped properties.</param>
        /// <returns>True, if at least one required option was not set.</returns>
        private static bool AreRequiredOptionsMapped(
            IEnumerable<OptionProperty> optionProperties,
            IEnumerable<OptionProperty> mappedProperties)
        {
            if(optionProperties == null)
                throw new ArgumentException($"{nameof(optionProperties)} cannot be null.");
            
            if(mappedProperties == null)
                throw new ArgumentNullException($"{nameof(mappedProperties)} cannot be null.");
            
            var requiredProperties = optionProperties
                .Where(op => op.Specification.IsRequired);

            var notSetProperties = requiredProperties
                .Select(op => op.Property)
                .Except(mappedProperties
                    .Select(mp => mp.Property));
            
            return !notSetProperties.Any();
        }

    }
    
}