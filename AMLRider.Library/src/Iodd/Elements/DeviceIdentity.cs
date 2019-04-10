using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;

namespace AMLRider.Library.Iodd.Elements
{
    
    public class DeviceIdentity : IoddElement
    {
        
        #region Attributes
        
        /// <summary>
        /// The vendor ID.
        /// </summary>
        public ushort VendorId { get; set; }
        
        /// <summary>
        /// The vendor name.
        /// </summary>
        public string VendorName { get; set; }
        
        /// <summary>
        /// The main device ID.
        /// </summary>
        public string DeviceId { get; set; }
        
        #endregion
        
        /// <summary>
        /// Additional device IDs given by the vendor.
        /// </summary>
        public List<string> AdditionalDeviceIds { get; set; } = new List<string>();

        #region Elements

        /// <summary>
        /// The vendor text.
        /// </summary>
        public string VendorText { get; set; }
        
        /// <summary>
        /// The vendor url.
        /// </summary>
        public string VendorUrl { get; set; }
        
        /// <summary>
        /// The vendor logo. This is optional.
        /// </summary>
        public string VendorLogo { get; set; }
        
        /// <summary>
        /// The device name.
        /// </summary>
        public string DeviceName { get; set; }
        
        /// <summary>
        /// The device family.
        /// </summary>
        public string DeviceFamily { get; set; }
        
        /// <summary>
        /// The list of device variants.
        /// </summary>
        private List<DeviceVariant> DeviceVariantCollection { get; set; } = new List<DeviceVariant>();
        
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