using System;

namespace AMLRider.Cli.Attributes
{
    
    /// <inheritdoc />
    /// <summary>
    /// Represents a command line verb.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class VerbAttribute : Attribute
    {

        /// <summary>
        /// The name of the verb.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The help text for the verb.
        /// </summary>
        public string HelpText { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="VerbAttribute" /> instance.
        /// </summary>
        /// <param name="name">The name of the verb.</param>
        public VerbAttribute(string name)
        {
//            if (string.IsNullOrWhiteSpace(name))
//                throw new ArgumentException($"The verb name is not allowed to be null or empty.", nameof(name));

            Name = name;
        }

    }
    
}