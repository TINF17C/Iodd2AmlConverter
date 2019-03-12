namespace AMLRider.Cli.Core
{

    /// <summary>
    /// Represents a token.
    /// A token can be either a verb, an option or a value.
    /// For example:
    /// <example>
    /// verb --option=value
    /// </example>
    /// </summary>
    public class Token
    {

        /// <summary>
        /// The type of the token.
        /// </summary>
        public TokenType Type { get; }

        /// <summary>
        /// The text of the token.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Creates a new <see cref="Token"/> instance.
        /// </summary>
        /// <param name="type">The type of the token.</param>
        /// <param name="text">The token text.</param>
        public Token(TokenType type, string text)
        {
            Type = type;
            Text = text;
        }

        /// <summary>
        /// Creates a <see cref="NameToken"/> from the specified text.
        /// </summary>
        /// <param name="text">The text to create a <see cref="NameToken"/> from.</param>
        /// <returns>A <see cref="NameToken"/>.</returns>
        public static NameToken Name(string text)
        {
            return new NameToken(text);
        }

        /// <summary>
        /// Creates a <see cref="ValueToken"/> from the specified text.
        /// </summary>
        /// <param name="text">The text to create a <see cref="ValueToken"/> from.</param>
        /// <returns>A <see cref="ValueToken"/>.</returns>
        public static ValueToken Value(string text)
        {
            return new ValueToken(text);
        }

    }

}