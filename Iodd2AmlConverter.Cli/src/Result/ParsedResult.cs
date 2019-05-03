namespace Iodd2AmlConverter.Cli.Result
{
    
    /// <inheritdoc />
    /// <summary>
    /// Represents the result returned by the command line parser if options where successfully parsed.
    /// </summary>
    public class ParsedResult : ParserResult
    {

        /// <summary>
        /// The result value, i.e. the parsed options in form of a previously specified class.
        /// </summary>
        public object Value { get; }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="ParsedResult" /> instance.
        /// </summary>
        /// <param name="value">The parsed options.</param>
        public ParsedResult(object value)
            : base(true)
        {
            Value = value;
        }

    }
    
}