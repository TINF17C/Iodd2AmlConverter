using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{

    public class ProfileBody : IoddElement
    {

        public DeviceIdentity DeviceIdentity { get; set; }

        public DeviceFunction DeviceFunction { get; set; }

        public override void Deserialize(XElement element)
        {
            if (element.SubElement("DeviceIdentity") != null)
            {
                DeviceIdentity = new DeviceIdentity();
                DeviceIdentity.Deserialize(element.SubElement("DeviceIdentity"));
            }

            if (element.SubElement("DeviceFunction") == null)
                return;

            DeviceFunction = new DeviceFunction();
            DeviceFunction.Deserialize(element.SubElement("DeviceFunction"));
        }

        public override AmlCollection ToAml()
        {
            var collection = new AmlCollection();

            if (DeviceIdentity != null)
                collection.Add(DeviceIdentity.ToAml().Cast<InternalElement>());

            if (DeviceFunction != null)
                collection.Add(DeviceFunction.ToAml().Cast<InternalElement>());

            return collection;
        }

    }

}