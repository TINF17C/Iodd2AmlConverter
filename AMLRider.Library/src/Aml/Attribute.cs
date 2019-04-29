using System.Xml.Linq;

namespace AMLRider.Library.Aml
{

    public class Attribute : AmlElement
    {

        public string Name { get; set; }

        public string AttributeDataType { get; set; }

        public Value Value { get; set; }

        public override XElement Serialize()
        {
            var element = new XElement("Attribute");
            element.SetAttributeValue("Name", Name);
            element.SetAttributeValue("AttributeDataType", AttributeDataType);

            var valueElement = Value.Serialize();
            element.Add(valueElement);

            return element;
        }

    }

}