using System;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    public class TransportLayers : IoddElement
    {
        public PhysicalLayer PhysicalLayer { get; set; }
        public override void Deserialize(XElement element)
        {
            PhysicalLayer.Deserialize(element);
        }

        public override AmlElement ToAml()
        {
            throw new NotImplementedException();
        }
    }
}