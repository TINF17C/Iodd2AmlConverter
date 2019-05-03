namespace Iodd2AmlConverter.Cli.Core
{
    
    /// <inheritdoc />
    /// <summary>
    /// Represents a name token.
    /// This can either be a verb or and option.
    /// </summary>
    public class NameToken : Token
    {

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="NameToken" /> instance.
        /// </summary>
        /// <param name="text">The name text.</param>
        public NameToken(string text)
            : base(TokenType.Name, text)
        {
        }

    }
    
}