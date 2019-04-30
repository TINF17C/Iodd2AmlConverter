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
            
            VendorText = element.SubElement("VendorText")?.GetAttributeValue("textId");
            VendorUrl = element.SubElement("VendorLogo")?.GetAttributeValue("textId");

            if (element.SubElement("VendorLogo") != null)
                VendorLogo = element.SubElement("VendorLogo").GetAttributeValue("name");

            DeviceName = element.SubElement("DeviceName")?.GetAttributeValue("textId");
            DeviceFamily = element.SubElement("DeviceFamily")?.GetAttributeValue("textId");
            
            DeviceVariantCollection = new DeviceVariantCollection();
            DeviceVariantCollection.Deserialize(element.SubElement("DeviceVariantCollection"));
        }

        public override AmlElement ToAml()
        {
            var element = new InternalElement
            {
                Name = "DeviceIdentity",
                Id = "DeviceIdentity"
            };
            
            element.Attributes.Add(CreateAttribute("vendorId", "integer", VendorId.ToString()));
            element.Attributes.Add(CreateAttribute("vendorName", "string", VendorName));
            element.Attributes.Add(CreateAttribute("deviceId", "integer", DeviceId));
            element.Attributes.Add(CreateAttribute("VendorText", "string", VendorText));
            element.Attributes.Add(CreateAttribute("VendorUrl", "anyURI", VendorUrl));
            element.Attributes.Add(CreateAttribute("DeviceFamily", "string", DeviceFamily));

            return element;
        }

        private static Attribute CreateAttribute(string name, string type, string value)
        {
            return new Attribute
            {
                Name = name,
                AttributeDataType = type,
                Value = new Value
                {
                    Content = value
                }
            };
        }
    }
    
}