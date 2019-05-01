using System.Xml.Linq;
using AMLRider.Library.Aml;

namespace AMLRider.Library.Iodd.Elements
{
    public class TransportLayers : IoddElement
    {
        public PhysicalLayer PhysicalLayer { get; set; }
        public override void Deserialize(XElement element)
        {
            PhysicalLayer.Deserialize();
        }

        public override AmlElement ToAml()
        {
            
        }
    }
}