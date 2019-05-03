namespace Iodd2AmlConverter.Cli.Core
{
    
    /// <inheritdoc />
    /// <summary>
    /// Represents a value token.
    /// </summary>
    public class ValueToken : Token
    {

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="ValueToken" /> instance.
        /// </summary>
        /// <param name="text">The value text.</param>
        public ValueToken(string text)
            : base(TokenType.Value, text)
        {
        }

    }
    
}