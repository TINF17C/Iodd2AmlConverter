using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    
    
    public class StdSingleValueRef : IoddElement
    {
        
        public string Value { get; set; }

        public override void Deserialize(XElement element)
        {
            Value = element.GetAttributeValue("value");
        }

        public override AmlElement ToAml()
        {
            var singleValueRef = new Attribute();
            var value = new Value
            {
                Content = Value
            };
            singleValueRef.Value = value;
            singleValueRef.AttributeDataType = "xs:integer";

            return singleValueRef;
        }
    }
}