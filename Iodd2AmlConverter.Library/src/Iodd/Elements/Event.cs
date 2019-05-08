using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{

    public class Event : IoddElement
    {

        public int Code { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        [Optional]
        public string Description { get; set; }

        public override void Deserialize(XElement element)
        {
            if (element.HasAttribute("code"))
                Code = int.Parse(element.GetAttributeValue("code"));

            Type = element.GetAttributeValue("type");
            Name = element.SubElement("Name")?.GetAttributeValue("textId");

            if (element.SubElement("Description") != null)
                Description = element.SubElement("Description").GetAttributeValue("textId");
        }

        public override AmlCollection ToAml()
        {
            // TODO:
            var attribute = new Attribute
            {
                Name = Code.ToString(),
                AttributeDataType = "xs:integer",
                DefaultValue = new DefaultValue
                {
                    Content = "0"
                }
            };

            return AmlCollection.Of(attribute);
        }

    }

}