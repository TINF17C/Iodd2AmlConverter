using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;

namespace AMLRider.Library.Iodd.Elements
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
            foreach (var deviceVariantElement in element.Elements("DeviceVariant"))
            {
                var deviceVariant = new DeviceVariant();
                deviceVariant.Deserialize(deviceVariantElement);
                
                DeviceVariants.Add(deviceVariant);
            }
        }

        public override AmlElement ToAml()
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