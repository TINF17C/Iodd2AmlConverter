using System;
using System.Collections.Generic;
using System.Linq;
using AMLRider.Cli.Core;

namespace AMLRider.Cli.Extensions
{
    
    /// <summary>
    /// Holds some helper methods for reflection functionality.
    /// </summary>
    public static class ReflectionExtensions
    {

        /// <summary>
        /// Gets all option properties of the current type.
        /// </summary>
        /// <param name="type">The type itself.</param>
        /// <returns>An enumerable of option properties.</returns>
        public static IEnumerable<OptionProperty> GetOptionProperties(this Type type)
        {
            return
                from property in type.GetProperties()
                select OptionProperty.FromProperty(property);
        }

    }
    
}