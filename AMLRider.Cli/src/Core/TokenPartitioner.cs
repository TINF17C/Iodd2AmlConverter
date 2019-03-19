using System.Collections.Generic;
using System.Linq;
using AMLRider.Cli.Extensions;

namespace AMLRider.Cli.Core
{
    
    /// <summary>
    /// Partitions tokens into key value pairs.
    /// </summary>
    public static class TokenPartitioner
    {

        /// <summary>
        /// Partitions tokens into key value pairs.
        /// </summary>
        /// <param name="tokens">The tokens to partition.</param>
        /// <returns>An enumerable of key value pairs.</returns>
        public static IEnumerable<KeyValuePair<string, string>> Partition(IEnumerable<Token> tokens)
        {
            return from pairs in tokens.Pairwise(
                    (key, value) =>
                        key.IsName() && value.IsValue()
                            ? new[] { new KeyValuePair<string, string>(key.Text, value.Text) }
                            : new KeyValuePair<string, string>[] { })
                   from pair in pairs
                   select pair;
        }

    }
    
}