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
            throw new System.NotImplementedException();
        }
        
    }
}