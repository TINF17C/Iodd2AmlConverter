using System;
using System.Linq;
using System.Reflection;
using Iodd2AmlConverter.Cli.Attributes;

namespace Iodd2AmlConverter.Cli.Core
{
    
    /// <summary>
    /// Models an option specification.
    /// </summary>
    public class OptionSpecification
    {

        /// <summary>
        /// The short name of the option.
        /// </summary>
        public char ShortName { get; }

        /// <summary>
        /// The long name of the option.
        /// </summary>
        public string LongName { get; }

        /// <summary>
        /// Specifies whether the option is required.
        /// </summary>
        public bool IsRequired { get; }

        /// <summary>
        /// Specifies the help text for the option.
        /// </summary>
        public string HelpText { get; }

        /// <summary>
        /// Creates a new <see cref="OptionSpecification"/> instance.
        /// </summary>
        /// <param name="shortName">The short name of the option.</param>
        /// <param name="longName">The long name of the option.</param>
        /// <param name="isRequired">Specifies whether the option is required.</param>
        /// <param name="helpText">Specifies the help text for the option.</param>
        private OptionSpecification(char shortName, string longName, bool isRequired, string helpText)
        {
            ShortName = shortName;
            LongName = longName;
            IsRequired = isRequired;
            HelpText = helpText;
        }

        /// <summary>
        /// Creates an <see cref="OptionSpecification"/> from a property.
        /// </summary>
        /// <param name="property">The property to create the specification from.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns>An <see cref="OptionSpecification"/>.</returns>
        public static OptionSpecification FromProperty(PropertyInfo property)
        {
            var attributes = property
                .GetCustomAttributes(true)
                .OfType<OptionAttribute>();

            var optionAttribute = attributes.FirstOrDefault();
            if(optionAttribute == null)
                throw new InvalidOperationException($"The specified property does not have an {nameof(OptionAttribute)}.");

            return new OptionSpecification(optionAttribute.ShortName, optionAttribute.LongName, optionAttribute.IsRequired, optionAttribute.HelpText);
        }

    }
    
}