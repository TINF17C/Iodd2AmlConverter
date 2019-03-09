using System.Collections.Generic;

namespace AMLRider.Cli.Result
{
    
    /// <inheritdoc />
    /// <summary>
    /// Represents the parser result if some options where not successfully parsed.
    /// </summary>
    public class NotParsedResult : ParserResult
    {

        /// <summary>
        /// The enumerable of error codes.
        /// </summary>
        public IEnumerable<int> Errors { get; }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="NotParsedResult" /> instance.
        /// </summary>
        public NotParsedResult()
            : base(false)
        {
        }

        /// <summary>
        /// Creates a new <see cref="NotParsedResult"/> instance.
        /// </summary>
        /// <param name="errors">A list of error codes.</param>
        public NotParsedResult(IEnumerable<int> errors)
            : base(false)
        {
            Errors = errors;
        }

    }
    
}