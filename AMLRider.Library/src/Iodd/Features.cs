using System.Collections.Generic;
using AMLRider.Library.Utils;

namespace AMLRider.Library.Iodd
{
    
    /// <summary>
    /// Describes features supported by a device.
    /// </summary>
    public class Features
    {
    
        #region Attributes
        
        /// <summary>
        /// Defines if a device supports the functionality of Block Parameter transmission.
        /// </summary>
        public bool BlockParameter { get; set; }
        
        /// <summary>
        /// Defines if a device supports data storage functionality.
        /// </summary>
        public bool DataStorage { get; set; }
        
        /// <summary>
        /// List of Profile Identifiers (PID) which are supported by this device
        /// </summary>
        public Optional<List<ushort>> ProfileCharacteristics { get; set; }
        
        #endregion

        #region Elements
        
        /// <summary>
        /// The supported access locks.
        /// </summary>
        public Optional<SupportedAccessLocks> SupportedAccessLocks { get; set; }

        #endregion
        
    }
    
}