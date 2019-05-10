using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;
using Iodd2AmlConverter.Library.Extensions;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{

    public class StdEventRef : IoddElement
    {

        public int Code { get; set; }

        public override void Deserialize(XElement element)
        {
            if (element.HasAttribute("code"))
                Code = int.Parse(element.GetAttributeValue("code"));
        }

        public override AmlCollection ToAml()
        {
            var stdEventRef = new Attribute
            {
                Name = Code.ToString(),
                AttributeDataType = "xs:integer",
                DefaultValue = new DefaultValue
                {
                    Content = "0"
                }
            };

            return AmlCollection.Of(stdEventRef);
        }

    }

}