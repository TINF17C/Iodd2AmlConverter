using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
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

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }

    }

}