using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    public class StdEventRef : IoddElement
    {
        public int Code { get; set; }
        
        public override void Deserialize(XElement element)
        {
            Code = int.Parse(element.GetAttributeValue("code"));
        }

        public override AmlCollection ToAml()
        {
            var stdEventRef = new Attribute();
            stdEventRef.Name = Code.ToString();
            stdEventRef.AttributeDataType = "xs:integer";
            stdEventRef.DefaultValue = new DefaultValue
            {
                Content = "0"
            };

            return AmlCollection.Of(stdEventRef);
        }
    }
}