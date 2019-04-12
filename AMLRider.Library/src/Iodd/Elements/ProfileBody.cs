using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{
    public class ProfileBody : IoddElement
    {
        
        public DeviceIdentity DeviceIdentity { get; set; }
        
        public DeviceFunction DeviceFunction { get; set; }
        
        public override void Deserialize(XElement element)
        {
            DeviceIdentity = new DeviceIdentity();
            DeviceIdentity.Deserialize(element.SubElement("DeviceIdentity"));
            
            DeviceFunction = new DeviceFunction();
            DeviceFunction.Deserialize(element.SubElement("DeviceFunction"));
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
    }
}