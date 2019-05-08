using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    public class DeviceVariantCollection : IoddElement, IEnumerable<DeviceVariant>
    {
        
        private List<DeviceVariant> DeviceVariants { get; set; }

        public DeviceVariantCollection()
        {
            DeviceVariants = new List<DeviceVariant>();
        }
        
        public override void Deserialize(XElement element)
        {
            foreach (var deviceVariantElement in element.SubElements("DeviceVariant"))
            {
                var deviceVariant = new DeviceVariant();
                deviceVariant.Deserialize(deviceVariantElement);
                
                DeviceVariants.Add(deviceVariant);
            }
        }

        public override AmlCollection ToAml()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<DeviceVariant> GetEnumerator()
        {
            return DeviceVariants.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}