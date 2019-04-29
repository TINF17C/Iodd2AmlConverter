using System;
using System.Xml.Linq;
using AMLRider.Library.Helpers;

namespace AMLRider.Library.Aml
{

    public class SystemUnitClass : AmlElement
    {

        public string Name { get; set; }
        
        public string Id { get; set; }


        public override XElement Serialize()
        {
            var element = XmlHelper.CreateElement("SystemUnitClass");
            element.SetAttributeValue("Name", Name);
            element.SetAttributeValue("ID", Id ?? Guid.NewGuid().ToString());

            return element;
        }

    }

}