using System.Xml.Linq;
using AMLRider.Library.Helpers;

namespace AMLRider.Library.Aml
{

    public class AmlName : AmlElement
    {
        
        public string Content { get; set; }
        
        public override XElement Serialize()
        {
            return XmlHelper.CreateElement("Name", Content ?? string.Empty);
        }

    }

}