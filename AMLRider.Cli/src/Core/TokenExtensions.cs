namespace AMLRider.Cli.Core
{
    
    /// <summary>
    /// Holds extension methods for the <see cref="Token"/> class.
    /// </summary>
    public static class TokenExtensions
    {

        /// <summary>
        /// Determines whether a token is a name.
        /// </summary>
        /// <param name="token">The token to check.</param>
        /// <returns>True, if the token is a name token.</returns>
        public static bool IsName(this Token token)
        {
            return token.Type == TokenType.Name;
        }

        /// <summary>
        /// Determines whether a token is a value.
        /// </summary>
        /// <param name="token">The token to check.</param>
        /// <returns>True, if the token is a value token.</returns>
        public static bool IsValue(this Token token)
        {
            return token.Type == TokenType.Value;
        }

    }
    
}