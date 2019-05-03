using System;
using System.Collections.Generic;
using System.Linq;
using Iodd2AmlConverter.Cli.Attributes;

namespace Iodd2AmlConverter.Cli.Core
{
    
    /// <summary>
    /// Represents a command line verb.
    /// For example
    /// <example>
    /// add,
    /// commit,
    /// push
    /// </example>
    /// </summary>
    public class Verb
    {

        /// <summary>
        /// The name of the verb.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The help text for the verb.
        /// </summary>
        public string HelpText { get; }

        /// <summary>
        /// Creates a new <see cref="Verb"/> instance.
        /// </summary>
        /// <param name="name">The name of the verb.</param>
        /// <param name="helpText">The help text of the verb.</param>
        private Verb(string name, string helpText)
        {
            Name = name;
            HelpText = helpText;
        }

        /// <summary>
        /// Creates a <see cref="Verb"/> from a <see cref="VerbAttribute"/>.
        /// </summary>
        /// <param name="attribute">The attribute to create the verb from.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>The created <see cref="Verb"/> instance.</returns>
        public static Verb FromAttribute(VerbAttribute attribute)
        {
            if(attribute == null)
                throw new ArgumentNullException(nameof(attribute));

            return new Verb(attribute.Name, attribute.HelpText);
        }

        /// <summary>
        /// Selects a number of verbs from the given types.
        /// </summary>
        /// <param name="types">The list of types.</param>
        /// <returns>A list of key value pairs containing the verb and the type.</returns>
        public static IEnumerable<KeyValuePair<Verb, Type>> SelectFromTypes(IEnumerable<Type> types)
        {
            return from type in types
                let attributes = type.GetCustomAttributes(typeof(VerbAttribute), true).ToArray()
                where attributes.Length == 1
                select new KeyValuePair<Verb, Type>(FromAttribute(attributes.Single() as VerbAttribute), type);
        }

    }
    
}