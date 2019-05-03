using System;

namespace Iodd2AmlConverter.Cli.Attributes
{
    
    /// <inheritdoc />
    /// <summary>
    /// Represents a command line option flag.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class OptionAttribute : Attribute
    {

        /// <summary>
        /// The one digit short name of the option.
        /// </summary>
        public char ShortName { get; }

        /// <summary>
        /// The full name of the option.
        /// </summary>
        public string LongName { get; }

        /// <summary>
        /// Specifies whether the option is required or not.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Specifies the help text of the option.
        /// </summary>
        public string HelpText { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="OptionAttribute" /> instance.
        /// </summary>
        /// <param name="shortName">The short name of the option.</param>
        /// <param name="longName">The long name of the option.</param>
        public OptionAttribute(char shortName, string longName)
        {
            if(!char.IsLetter(shortName))
                throw new ArgumentException($"{nameof(shortName)} has to be a letter.", nameof(shortName));

            if(string.IsNullOrWhiteSpace(longName))
                throw new ArgumentException($"{nameof(longName)} is not allowed to be null or empty.", nameof(longName));

            ShortName = shortName;
            LongName = longName;
        }

    }
}