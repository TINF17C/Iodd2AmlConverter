using System.Xml.Linq;
using Iodd2AmlConverter.Library.Helpers;

namespace Iodd2AmlConverter.Library.Aml
{

    public class SystemUnitClassLib : AmlElement
    {

        public string Name { get; set; }
        
        public SystemUnitClass SystemUnitClass { get; set; }

        public override XElement Serialize()
        {
            var element = XmlHelper.CreateElement("SystemUnitClassLib");
            element.SetAttributeValue("Name", Name);

            var systemUnitClassElement = SystemUnitClass.Serialize();
            element.Add(systemUnitClassElement);

            return element;
        }

    }

}