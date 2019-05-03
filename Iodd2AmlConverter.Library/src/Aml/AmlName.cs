using System.Xml.Linq;
using Iodd2AmlConverter.Library.Helpers;

namespace Iodd2AmlConverter.Library.Aml
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