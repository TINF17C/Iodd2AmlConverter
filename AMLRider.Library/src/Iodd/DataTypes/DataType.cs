using System.Xml.Linq;
using AMLRider.Library.Extensions;
using AMLRider.Library.Utils;

namespace AMLRider.Library.Iodd.DataTypes
{
    
    /// <summary>
    /// Definition of a data type.
    /// </summary>
    public class DataType
    {
        
        #region Attributes
        
        /// <summary>
        /// The data type ID.
        /// </summary>
        public Optional<string> Id { get; set; }
        
        #endregion

        
    }
    
}