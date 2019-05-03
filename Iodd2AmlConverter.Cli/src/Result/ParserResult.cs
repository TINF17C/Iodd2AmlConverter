using System;
using System.Collections.Generic;

namespace Iodd2AmlConverter.Cli.Result
{
    
    /// <summary>
    /// Represents a basic result returned by the command line parser.
    /// </summary>
    public abstract class ParserResult
    {

        /// <summary>
        /// Indicates whether the command line parser successfully completed.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// The list of parsed types.
        /// </summary>
        private IEnumerable<Type> TypeChoices { get; }

        /// <summary>
        /// Creates a new <see cref="ParserResult"/> instance.
        /// </summary>
        /// <param name="success">Indicates whether the command line parser successfully completed.</param>
        protected ParserResult(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// Creates a new <see cref="ParserResult"/> instance.
        /// </summary>
        /// <param name="success">Indicates whether the command line parser successfully completed.</param>
        /// <param name="typeChoices">The list of parsed types.</param>
        protected ParserResult(bool success, IEnumerable<Type> typeChoices)
        {
            Success = success;
            TypeChoices = typeChoices;
        }

    }
    
}