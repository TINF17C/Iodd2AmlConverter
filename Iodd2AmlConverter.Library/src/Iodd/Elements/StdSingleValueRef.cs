using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;
using Iodd2AmlConverter.Library.Extensions;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    
    
    public class StdSingleValueRef : IoddElement
    {
        
        public string Value { get; set; }

        public override void Deserialize(XElement element)
        {
            Value = element.GetAttributeValue("value");
        }

        public override AmlCollection ToAml()
        {
            var attribute = new Attribute
            {
                Name = "StdSingleValueRef",
                AttributeDataType = "xs:integer",
                Value = new Value
                {
                    Content = Value
                }
            };
            
            return AmlCollection.Of(attribute);
        }
    }
}