using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
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