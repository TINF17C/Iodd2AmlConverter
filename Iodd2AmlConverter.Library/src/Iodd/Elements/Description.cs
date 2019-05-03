using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    public class Description : IoddElement
    {
        
        public string TextId { get; set; }

        public override void Deserialize(XElement element)
        {
            TextId = element.GetAttributeValue("textId");
        }

        public override AmlElement ToAml()
        {
            // TODO: Resolve TextId from ExternalTextCollection
            return new AmlDescription
            {
                Content = TextId
            };
        }
        
    }
}