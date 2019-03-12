using System;
using AMLRider.Cli.Result;

namespace AMLRider.Cli.Extensions
{
    
    /// <summary>
    /// Extension methods for the <see cref="ParserResult"/> class.
    /// </summary>
    public static class ParserResultExtensions
    {

        /// <summary>
        /// Runs an action if the command line parser parsed the options of the given type.
        /// </summary>
        /// <typeparam name="T">The type of the class where option attributes where specified.</typeparam>
        /// <param name="result">The parser result itself.</param>
        /// <param name="action">The action which is executed if options of the specified type had been parsed.</param>
        public static ParserResult WithParsed<T>(this ParserResult result, Action<T> action)
        {
            if(action == null)
                throw new ArgumentNullException(nameof(action));

            if (!(result is ParsedResult parsedResult))
                return result;

            if (!(parsedResult.Value is T))
                return result;

            action((T)parsedResult.Value);
            return result;
        }

    }
    
}