using System;
using System.Collections.Generic;
using System.Linq;

namespace AMLRider.Cli.Core
{
    
    /// <summary>
    /// Parses command line arguments as tokens.
    /// </summary>
    public class Tokenizer
    {

        /// <summary>
        /// Parses command line arguments as tokens.
        /// </summary>
        /// <param name="arguments">The command line arguments.</param>
        /// <returns>An enumerable of tokens.</returns>
        public static IEnumerable<Token> Tokenize(IEnumerable<string> arguments)
        {
            return
                from arg in arguments
                from token in !arg.StartsWith("-", StringComparison.Ordinal)
                    ? new[] { Token.Value(arg) }
                    : arg.StartsWith("--", StringComparison.Ordinal)
                        ? TokenizeLongName(arg)
                        : TokenizeShortName(arg)
                select token;
        }

        /// <summary>
        /// Parses a short name option.
        /// </summary>
        /// <param name="value">The string value of the switch.</param>
        /// <returns>An enumerable of tokens.</returns>
        private static IEnumerable<Token> TokenizeShortName(string value)
        {
            if (value.Length <= 1 || value[0] != '-' || value[1] == '-')
                yield break;

            var text = value.Substring(1);

            if (!text.Contains("="))
            {
                yield return Token.Name(text);
                yield break;
            }

            var textSplit = text.Split('=');

            yield return Token.Name(textSplit[0]);
            yield return Token.Value(textSplit[1]);
        }

        /// <summary>
        /// Parses a long name option.
        /// </summary>
        /// <param name="value">The string value of the switch.</param>
        /// <returns>An enumerable of tokens.</returns>
        private static IEnumerable<Token> TokenizeLongName(string value)
        {
            if (value.Length <= 2 || !value.StartsWith("--"))
                yield break;

            var text = value.Substring(2);
            if (!text.Contains("="))
            {
                yield return Token.Name(text);
                yield break;
            }

            var textSplit = text.Split('=');

            yield return Token.Name(textSplit[0]);
            yield return Token.Value(textSplit[1]);
        }

    }
    
}