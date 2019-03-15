using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AMLRider.Cli.Attributes;
using AMLRider.Cli.Core;
using AMLRider.Cli.Extensions;

namespace AMLRider.Cli
{
    
    /// <summary>
    /// Specifies settings for the help generator.
    /// </summary>
    public class HelpGeneratorSettings
    {
        
        /// <summary>
        /// Specifies how much characters the option list should be indented.
        /// </summary>
        public int OptionIndentation { get; set; } = 3;

    }

    /// <summary>
    /// Responsible for generating formatted help texts.
    /// </summary>
    public class HelpGenerator
    {

        /// <summary>
        /// The settings used by the help generator.
        /// </summary>
        private HelpGeneratorSettings Settings { get; }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="T:AMLRider.Cli.HelpGenerator" /> instance.
        /// </summary>
        public HelpGenerator()
            : this(new HelpGeneratorSettings())
        {
        }

        /// <summary>
        /// Creates a new <see cref="HelpGenerator"/> instance.
        /// </summary>
        /// <param name="settings">The <see cref="HelpGeneratorSettings"/>.</param>
        public HelpGenerator(HelpGeneratorSettings settings)
        {
            Settings = settings;
        }
        
        /// <summary>
        /// Generates a formatted help text for the given type.
        /// </summary>
        /// <param name="optionType">The type.</param>
        /// <returns>A formatted text as a <see cref="string"/>.</returns>
        public string Generate(Type optionType)
        {
            var verbAttribute = GetVerbAttribute(optionType);
            var optionProperties = optionType.GetOptionProperties();

            var stringBuilder = new StringBuilder();
            if (verbAttribute != null)
            {
                stringBuilder
                    .Append(verbAttribute.Name)
                    .Append(":")
                    .Append(" ");
                
                stringBuilder.AppendLine(verbAttribute.HelpText);
            }


            var maxOptionNameLength = GetLongestOptionName(optionProperties);
            foreach (var property in optionProperties)
            {
                var spec = property.Specification;
                stringBuilder
                    .Append(' ', Settings.OptionIndentation)
                    .Append("- ")
                    .Append(spec.LongName)
                    .Append(' ', 1 + maxOptionNameLength - spec.LongName.Length)
                    .Append($"({spec.ShortName})")
                    .Append(' ')
                    .Append(spec.HelpText)
                    .Append(spec.IsRequired ? " [Required]" : "")
                    .AppendLine();
            }


            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns the longest option name of the given option properties.
        /// </summary>
        /// <param name="optionProperties">A list of option properties.</param>
        /// <returns>The length of the longest name.</returns>
        private static int GetLongestOptionName(IEnumerable<OptionProperty> optionProperties)
        {
            return optionProperties
                .Select(prop => prop.Specification)
                .Select(spec => spec.LongName.Length)
                .Concat(new[] { int.MinValue }).Max();
        }
        
        /// <summary>
        /// Gets the <see cref="VerbAttribute"/> from a specific type.
        /// </summary>
        /// <param name="optionType">The type.</param>
        /// <returns>The <see cref="VerbAttribute"/> the type is marked with.</returns>
        private static VerbAttribute GetVerbAttribute(ICustomAttributeProvider optionType)
        {
            var typeAttributes = optionType.GetCustomAttributes(typeof(VerbAttribute), false);
            var verbAttribute = typeAttributes.FirstOrDefault() as VerbAttribute;

            return verbAttribute;
        }

    }
    
}