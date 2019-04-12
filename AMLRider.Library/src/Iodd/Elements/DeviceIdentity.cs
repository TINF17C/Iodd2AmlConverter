using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

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
        
        /// <summary>
        /// Additional device IDs given by the vendor.
        /// </summary>
        [Optional]
        public List<string> AdditionalDeviceIds { get; set; }
        
        #endregion

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
        [Optional]
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
        private DeviceVariantCollection DeviceVariantCollection { get; set; }
        
        #endregion

        public override void Deserialize(XElement element)
        {
            VendorId = ushort.Parse(element.GetAttributeValue("vendorId"));
            VendorName = element.GetAttributeValue("vendorName");
            DeviceId = element.GetAttributeValue("deviceId");
            
            // TODO: AdditionalDeviceIds
            
            VendorText = element.Element("VendorText")?.GetAttributeValue("textId");
            VendorUrl = element.Element("VendorLogo")?.GetAttributeValue("textId");

            if (element.Element("VendorLogo") != null)
                VendorLogo = element.Element("VendorLogo").GetAttributeValue("name");

            DeviceName = element.Element("DeviceName")?.GetAttributeValue("textId");
            DeviceFamily = element.Element("DeviceFamily")?.GetAttributeValue("textId");
            
            DeviceVariantCollection = new DeviceVariantCollection();
            DeviceVariantCollection.Deserialize(element.Element("DeviceVariantCollection"));
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
    }
    
}