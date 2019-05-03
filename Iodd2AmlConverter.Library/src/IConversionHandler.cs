namespace Iodd2AmlConverter.Library
{
    /// <summary>
    /// Responsible for converting IODD files into AML files.
    /// </summary>
    public interface IConversionHandler
    {
        /// <summary>
        /// Converts an IODD file into an AML file.
        /// </summary>
        /// <param name="ioddFileData">The content of an IODD file as a string.</param>
        /// <returns>Returns a string which contains the generated AML data.</returns>
        string Convert(string ioddFileData);

    }
}