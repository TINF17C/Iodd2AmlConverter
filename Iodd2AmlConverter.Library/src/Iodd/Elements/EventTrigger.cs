using System;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Extensions;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{

    public class EventTrigger : IoddElement
    {

        public byte AppearValue { get; set; }
        
        public byte DisappearValue { get; set; }
        
        public override void Deserialize(XElement element)
        {
            AppearValue = byte.Parse(element.GetAttributeValue("appearValue"));
            DisappearValue = byte.Parse(element.GetAttributeValue("disappearValue"));
        }

        public override AmlCollection ToAml()
        {
            return AmlCollection.Emtpy();
        }

    }

}