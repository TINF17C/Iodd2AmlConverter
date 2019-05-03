using System;
using System.Xml;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    public class CommNetworkProfile : IoddElement
    {
        public string Datatype { get; set; }
        public string IOLinkRevision { get; set; }
        public string CompatibleWith { get; set; }
        public TransportLayers TransportLayers { get; set; }
        public Test Test { get; set; }
        
        public override void Deserialize(XElement element)
        {
            Datatype = element.Attribute(element.GetNamespaceOfPrefix("xsi") + "type")?.Value;
            IOLinkRevision = "V1.1";
            if (element.Attribute("compatibleWith") != null)
                CompatibleWith = "V1.0";
            //TransportLayers.Deserialize();
            //Test.Deserialize();
        }

        public override AmlElement ToAml()
        {
            throw new NotImplementedException();
        }
    }
}