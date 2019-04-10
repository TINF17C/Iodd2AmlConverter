using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Utils;

namespace AMLRider.Library.Iodd.Elements
{
    
    /// <summary>
    /// Describes features supported by a device.
    /// </summary>
    public class Features : IoddElement
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
        [Optional]
        public List<ushort> ProfileCharacteristics { get; set; }
        
        #endregion

        #region Elements
        
        /// <summary>
        /// The supported access locks.
        /// </summary>
        [Optional]
        public SupportedAccessLocks SupportedAccessLocks { get; set; }

        #endregion

        public override void Deserialize(XElement element)
        {
            throw new System.NotImplementedException();
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
        
    }
    
}