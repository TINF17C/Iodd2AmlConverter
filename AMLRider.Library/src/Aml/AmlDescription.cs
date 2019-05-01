using System.Xml.Linq;
using AMLRider.Library.Helpers;

namespace AMLRider.Library.Aml
{

    public class AmlDescription : AmlElement
    {
        
        public string Content { get; set; }
        
        public override XElement Serialize()
        {
            return XmlHelper.CreateElement("Description", Content ?? string.Empty);
        }

    }

}