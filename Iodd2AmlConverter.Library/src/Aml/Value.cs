using System.Xml.Linq;
using Iodd2AmlConverter.Library.Helpers;

namespace Iodd2AmlConverter.Library.Aml
{

    public class Value : AmlElement
    {

        public string Content { get; set; }

        public override XElement Serialize()
        {
            return XmlHelper.CreateElement("Value", Content ?? string.Empty);
        }

    }

}