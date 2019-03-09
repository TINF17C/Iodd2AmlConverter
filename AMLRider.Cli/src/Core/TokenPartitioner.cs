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

        /// <summary>
        /// Partitions and determines tokens which are switches (booleans).
        /// </summary>
        /// <param name="tokens">The tokens to partition.</param>
        /// <returns>The switches as key value pairs.</returns>
        /// // TODO: If switch is no boolean, then problem ...
        private static IEnumerable<KeyValuePair<string, string>> PartitionSwitches(IEnumerable<Token> tokens)
        {
            var tokenArray = tokens.ToArray();
            for (var i = 0; i < tokenArray.Length; ++i)
            {
                var token = tokenArray[i];
                if (!token.IsName())
                    continue;

                var nextIndex = i + 1;
                if (nextIndex >= tokenArray.Length)
                {
                    yield return new KeyValuePair<string, string>(token.Text, true.ToString());
                    continue;
                }

                var nextToken = tokenArray[nextIndex];
                if (!nextToken.IsName())
                    continue;

                ++i;
                yield return new KeyValuePair<string, string>(token.Text, true.ToString());
            }
        }

    }
    
}