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
            DeviceIdentity = new DeviceIdentity();
            DeviceIdentity.Deserialize(element.SubElement("DeviceIdentity"));
            
            DeviceFunction = new DeviceFunction();
            DeviceFunction.Deserialize(element.SubElement("DeviceFunction"));
        }

        public override AmlElement ToAml()
        {
            var systemClass = new SystemUnitClass
            {
                Name = "Device"
            };

            var identityElement = DeviceIdentity.ToAml() as InternalElement;
            systemClass.InternalElements.Add(identityElement);

            var functionElement = DeviceFunction.ToAml() as InternalElement;
            systemClass.InternalElements.Add(functionElement);
            
            return new SystemUnitClassLib
            {
                Name = "ComponentSystemUnitClassLib",
                SystemUnitClass = systemClass
            };
        }
    }
}