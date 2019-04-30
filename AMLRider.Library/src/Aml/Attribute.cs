using System.Collections.Generic;
using System.Xml.Linq;

namespace AMLRider.Library.Aml
{

    public class Attribute : AmlElement
    {

        public string Name { get; set; }

        public string AttributeDataType { get; set; }
        
        public DefaultValue DefaultValue { get; set; }

        public Value Value { get; set; }
        
        public List<Attribute> Attributes { get; set; }

        public Attribute()
        {
            Attributes = new List<Attribute>();
        }
        
        public override XElement Serialize()
        {
            var element = new XElement("Attribute");
            element.SetAttributeValue("Name", Name);
            element.SetAttributeValue("AttributeDataType", AttributeDataType);

            var defaultValueElement = DefaultValue.Serialize();
            element.Add(defaultValueElement);
            
            var valueElement = Value.Serialize();
            element.Add(valueElement);

            foreach (var attribute in Attributes)
            {
                var attributeElement = attribute.Serialize();
                element.Add(attributeElement);
            }
            
            return element;
        }

    }

}