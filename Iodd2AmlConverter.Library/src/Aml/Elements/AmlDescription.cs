using System.Xml.Linq;
using Iodd2AmlConverter.Library.Helpers;

namespace Iodd2AmlConverter.Library.Aml.Elements
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