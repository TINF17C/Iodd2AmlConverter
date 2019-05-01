using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{
    public class Event : IoddElement
    {
        public int Code { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        [Optional]
        public string Description { get; set; }
        
        public override void Deserialize(XElement element)
        {
            Code = int.Parse(element.GetAttributeValue("code"));
            Type = element.GetAttributeValue("type");
            Name = element.SubElement("Name").GetAttributeValue("textId");
            if (element.SubElement("Description") != null)
                Description = element.SubElement("Description").GetAttributeValue("textId");
        }

        public override AmlElement ToAml()
        {
            // TODO
            var test = new Attribute();
            return test;
        }
    }
}