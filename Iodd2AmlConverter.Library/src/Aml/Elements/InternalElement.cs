using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Iodd2AmlConverter.Library.Aml.Elements
{

    public class InternalElement : AmlElement
    {

        public string Name { get; set; }

        public string Id { get; set; }

        public SupportedRoleClass SupportedRoleClass { get; set; }

        public List<Attribute> Attributes { get; set; }
        
        public List<InternalElement> InternalElements { get; set; }
        
        public List<ExternalInterface> ExternalInterfaces { get; set; }
        
        public AmlDescription AmlDescription { get; set; }

        public InternalElement()
        {
            Attributes = new List<Attribute>();
            InternalElements = new List<InternalElement>();
            ExternalInterfaces = new List<ExternalInterface>();
        }

        public override XElement Serialize()
        {
            var element = new XElement("InternalElement");
            element.SetAttributeValue("Name", Name);
            element.SetAttributeValue("ID", Id ?? Guid.NewGuid().ToString());

            foreach (var attribute in Attributes)
            {
                var attributeElement = attribute.Serialize();
                element.Add(attributeElement);
            }

            foreach (var internalElement in InternalElements)
            {
                var xmlElement = internalElement.Serialize();
                element.Add(xmlElement);
            }

            foreach (var externalInterface in ExternalInterfaces)
            {
                var interfaceElement = externalInterface.Serialize();
                element.Add(interfaceElement);
            }
            
            if (SupportedRoleClass != null)
            {
                var roleClassElement = SupportedRoleClass.Serialize();
                element.Add(roleClassElement);
            }
            
            if (AmlDescription != null)
            {
                var descriptionElement = AmlDescription.Serialize();
                element.Add(descriptionElement);
            }

            return element;
        }

    }

}