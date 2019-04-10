using System.Xml.Linq;
using AMLRider.Library.Aml;

namespace AMLRider.Library.Iodd.Elements
{
    public class ProfileBody : IoddElement
    {
        
        public DeviceIdentity DeviceIdentity { get; set; }
        
        public DeviceFunction DeviceFunction { get; set; }
        
        public override void Deserialize(XElement element)
        {
            DeviceIdentity = new DeviceIdentity();
            DeviceIdentity.Deserialize(element.Element("DeviceIdentity"));
            
            DeviceFunction = new DeviceFunction();
            DeviceFunction.Deserialize(element.Element("DeviceFunction"));
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
    }
}