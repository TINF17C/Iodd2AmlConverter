using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Helpers;

namespace Iodd2AmlConverter.Library.Aml.Elements
{

    public class SystemUnitClass : AmlElement
    {

        public string Name { get; set; }
        
        public string Id { get; set; }
        
        public List<InternalElement> InternalElements { get; set; }

        public SystemUnitClass()
        {
            InternalElements = new List<InternalElement>();
        }
        
        public override XElement Serialize()
        {
            var element = XmlHelper.CreateElement("SystemUnitClass");
            element.SetAttributeValue("Name", Name);
            element.SetAttributeValue("ID", Id ?? Guid.NewGuid().ToString());

            foreach (var internalElement in InternalElements)
            {
                var xmlElement = internalElement.Serialize();
                element.Add(xmlElement);
            }

            return element;
        }

    }

}