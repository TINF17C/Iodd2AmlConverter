using System.Collections.Generic;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;

namespace Iodd2AmlConverter.Library.Iodd.Elements
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
            if(element.HasAttribute("vendorId"))
                VendorId = ushort.Parse(element.GetAttributeValue("vendorId"));
            
            if(element.HasAttribute("vendorName"))
                VendorName = element.GetAttributeValue("vendorName");
            
            if(element.HasAttribute("deviceId"))
                DeviceId = element.GetAttributeValue("deviceId");

            // TODO: AdditionalDeviceIds

            VendorText = element.SubElement("VendorText")?.GetAttributeValue("textId");
            VendorUrl = element.SubElement("VendorUrl")?.GetAttributeValue("textId");

            if (element.SubElement("VendorLogo") != null)
                VendorLogo = element.SubElement("VendorLogo").GetAttributeValue("name");

            DeviceName = element.SubElement("DeviceName")?.GetAttributeValue("textId");
            DeviceFamily = element.SubElement("DeviceFamily")?.GetAttributeValue("textId");

            if (element.SubElement("DeviceVariantCollection") == null)
                return;
            
            DeviceVariantCollection = new DeviceVariantCollection();
            DeviceVariantCollection.Deserialize(element.SubElement("DeviceVariantCollection"));
        }

        public override AmlCollection ToAml()
        {
            var element = new InternalElement
            {
                Name = "DeviceIdentity",
                Id = "DeviceIdentity"
            };

            element.Attributes.Add(CreateAttribute("vendorId", "xs:integer", VendorId.ToString()));
            element.Attributes.Add(CreateAttribute("vendorName", "xs:string", VendorName));
            element.Attributes.Add(CreateAttribute("deviceId", "xs:integer", DeviceId));
            element.Attributes.Add(CreateAttribute("VendorText", "xs:string", VendorText));
            element.Attributes.Add(CreateAttribute("VendorUrl", "xs:anyURI", VendorUrl));
            element.Attributes.Add(CreateAttribute("DeviceFamily", "xs:string", DeviceFamily));
            element.Attributes.Add(CreateAttribute("DeviceName", "xs:string", DeviceName));

            return AmlCollection.Of(element, ConstructVendorLogo());
        }

        private InternalElement ConstructVendorLogo()
        {
            var element = new InternalElement
            {
                Name = "ManufacturerIcon",
                SupportedRoleClass = new SupportedRoleClass
                {
                    RefRoleClassPath = "AutomationMLComponentStandardRCL/ManufacturerIcon"
                }
            };

            var externalInterface = new ExternalInterface
            {
                Name = "ExternalDataConnector",
                RefBaseClassPath = "AutomationMLInterfaceClassLib/AutomationMLBaseInterface/ExternalDataConnector",
                Attribute = new Attribute
                {
                    Name = "refURI",
                    AttributeDataType = "xs:anyURI",
                    Value = new Value
                    {
                        Content = "/" + VendorLogo
                    }
                }
            };

            element.ExternalInterfaces.Add(externalInterface);
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