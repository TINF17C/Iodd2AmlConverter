using System;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Extensions;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    public class DeviceVariant : IoddElement
    {
        #region Attributes

        public string ProductId { get; set; }

        [Optional]
        public string DeviceSymbol { get; set; }

        [Optional]
        public string DeviceIcon { get; set; }

        #endregion

        #region Elements

        public Name Name { get; set; }

        public Description Description { get; set; }

        #endregion

        public override void Deserialize(XElement element)
        {
            ProductId = element.GetAttributeValue("productId");
            DeviceSymbol = element.GetAttributeValue("deviceSymbol");
            DeviceIcon = element.GetAttributeValue("deviceIcon");

            if (element.SubElement("Name") != null)
            {
                Name = new Name();
                Name.Deserialize(element.SubElement("Name"));
            }

            if (element.SubElement("Description") == null)
                return;
            
            Description = new Description();
            Description.Deserialize(element.SubElement("Description"));
        }

        public override AmlCollection ToAml()
        {
            return AmlCollection.Emtpy();
        }
    }
}