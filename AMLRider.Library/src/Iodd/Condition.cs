using System.Runtime.CompilerServices;
using AMLRider.Library.Utils;

namespace AMLRider.Library.Iodd
{
    
    /// <summary>
    /// Serves to switch between different process data.
    /// </summary>
    public class Condition
    {
        
        /// <summary>
        /// References a variable.
        /// </summary>
        public string VariableId { get; set; }
        
        /// <summary>
        /// Addresses the record item within the record.
        /// </summary>
        public Optional<byte> SubIndex { get; set; }
        
        /// <summary>
        /// The value of a variable.
        /// </summary>
        public byte Value { get; set; }
        
    }
}