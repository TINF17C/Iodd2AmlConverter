using System;

namespace AMLRider.Library
{
    /// <inheritdoc />
    /// <summary>
    /// Responsible for converting IODD files into AML files.
    /// </summary>
    public class ConversionHandler : IConversionHandler
    {
        /// <inheritdoc />
        /// <summary>
        /// Converts an IODD file into an AML file.
        /// </summary>
        /// <param name="ioddFileData">The content of an IODD file as a string.</param>
        /// <returns>Returns a string which contains the generated AML data.</returns>
        public string Convert(string ioddFileData)
        {
            return "";
        }
    }
}