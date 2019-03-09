using System;
using System.Reflection;

namespace AMLRider.Cli.Core
{
    
    /// <summary>
    /// Models a property with its corresponding option specification.
    /// </summary>
    public class OptionProperty
    {

        /// <summary>
        /// The property.
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// The option specification.
        /// </summary>
        public OptionSpecification Specification { get; }

        /// <summary>
        /// Creates a new <see cref="OptionProperty"/> instance.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="specification">The option specification.</param>
        private OptionProperty(PropertyInfo property, OptionSpecification specification)
        {
            Property = property;
            Specification = specification;
        }

        /// <summary>
        /// Creates a new <see cref="OptionProperty"/> from a property.
        /// </summary>
        /// <param name="property">The property to create the <see cref="OptionProperty"/> from.</param>
        /// <returns>A new <see cref="OptionProperty"/> instance.</returns>
        public static OptionProperty FromProperty(PropertyInfo property)
        {
            if(property == null)
                throw new ArgumentNullException(nameof(property));

            var specification = OptionSpecification.FromProperty(property);
            return new OptionProperty(property, specification);
        }

    }
    
}