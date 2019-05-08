using System.Collections.Generic;
using System.Xml.Linq;

namespace Iodd2AmlConverter.Library.Aml.Elements
{

    public class Attribute : AmlElement
    {

        public string Name { get; set; }

        public string AttributeDataType { get; set; }

        public DefaultValue DefaultValue { get; set; }

        public Value Value { get; set; }

        public List<Attribute> Attributes { get; set; }
        
        public RefSemantic RefSemantic { get; set; }

        public Attribute()
        {
            Attributes = new List<Attribute>();
        }

        public override XElement Serialize()
        {
            var element = new XElement("Attribute");
            element.SetAttributeValue("Name", Name);
            element.SetAttributeValue("AttributeDataType", AttributeDataType);

            if (RefSemantic != null)
            {
                var refElement = RefSemantic.Serialize();
                element.Add(refElement);
            }
            
            if (DefaultValue != null)
            {
                var defaultValueElement = DefaultValue.Serialize();
                element.Add(defaultValueElement);
            }

            if (Value != null)
            {
                var valueElement = Value.Serialize();
                element.Add(valueElement);
            }

            foreach (var attribute in Attributes)
            {
                var attributeElement = attribute.Serialize();
                element.Add(attributeElement);
            }

            return element;
        }

    }

}