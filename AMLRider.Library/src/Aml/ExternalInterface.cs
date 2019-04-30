using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AMLRider.Library.Aml
{

    public class ExternalInterface : AmlElement
    {

        private const string DefaultRefBaseClassPath =
            "AutomationMLInterfaceClassLib/AutomationMLBaseInterface/ExternalDataConnector";
        
        public string Name { get; set; }
        
        public string Id { get; set; }
        
        public string RefBaseClassPath { get; set; }
        
        public Attribute Attribute { get; set; }

        public override XElement Serialize()
        {
            var element = new XElement("ExternalInterface");
            element.SetAttributeValue("Name", Name);
            element.SetAttributeValue("ID", Id ?? Guid.NewGuid().ToString());
            element.SetAttributeValue("RefBaseClassPath", RefBaseClassPath ?? DefaultRefBaseClassPath);

            var attributeElement = Attribute.Serialize();
            element.Add(attributeElement);

            return element;
        }

    }

}