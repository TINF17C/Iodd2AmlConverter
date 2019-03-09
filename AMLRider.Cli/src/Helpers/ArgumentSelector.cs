using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AMLRider.Cli.Helpers
{
    
    /// <summary>
    /// Responsible for converting a line of text to command line arguments.
    /// </summary>
    public static class ArgumentSelector
    {

        /// <summary>
        /// The argument regex responsible for separating the line into single arguments.
        /// </summary>
        private static Regex ArgRegex { get; }

        /// <summary>
        /// Static constructor of <see cref="ArgumentSelector"/>.
        /// </summary>
        static ArgumentSelector()
        {
            ArgRegex = new Regex(@"[\""].+?[\""]|[^ ]+", RegexOptions.Compiled);
        }

        /// <summary>
        /// Turns a line of text into separate command line arguments
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static IEnumerable<string> Select(string line)
        {
            return ArgRegex.Matches(line)
                .Cast<Match>()
                .Select(m => m.Value)
                .Select(s => s.Replace(@"""", ""))
                .ToArray();
        }

    }
    
}