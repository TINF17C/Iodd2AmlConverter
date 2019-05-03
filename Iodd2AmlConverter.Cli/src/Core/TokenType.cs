namespace Iodd2AmlConverter.Cli.Core
{
    
    /// <summary>
    /// Specifies the type of a token.
    /// </summary>
    public enum TokenType
    {

        /// <summary>
        /// Unknown token type.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Specifies that the token is a name token.
        /// This can be either a verb or an option name.
        /// </summary>
        Name = 1,

        /// <summary>
        /// Specifies that the token is a value token.
        /// </summary>
        Value = 2

    }
    
}