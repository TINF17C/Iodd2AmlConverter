using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    public class Description : IoddElement
    {
        
        public string TextId { get; set; }

        public override void Deserialize(XElement element)
        {
            TextId = element.GetAttributeValue("textId");
        }

        public override AmlCollection ToAml()
        {
            var element = new AmlDescription
            {
                Content = TextId
            };
            
            return AmlCollection.Of(element);
        }
        
    }
}