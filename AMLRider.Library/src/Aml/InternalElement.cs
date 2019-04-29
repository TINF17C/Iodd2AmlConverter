using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AMLRider.Library.Aml
{

    public class InternalElement : AmlElement
    {

        public string Name { get; set; }
        
        public string Id { get; set; }
        
        public SupportedRoleClass SupportedRoleClass { get; set; }
        
        public List<ExternalInterface> ExternalInterfaces { get; set; }

        public InternalElement()
        {
            ExternalInterfaces = new List<ExternalInterface>();
        }

        public override XElement Serialize()
        {
            var element = new XElement("InternalElement");
            element.SetAttributeValue("Name", Name);
            element.SetAttributeValue("ID", Id ?? Guid.NewGuid().ToString());

            var roleClassElement = SupportedRoleClass.Serialize();
            element.Add(roleClassElement);
            
            foreach (var externalInterface in ExternalInterfaces)
            {
                var interfaceElement = externalInterface.Serialize();
                element.Add(interfaceElement);
            }

            return element;
        }

    }

}