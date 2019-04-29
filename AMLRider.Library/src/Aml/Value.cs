using System.Xml.Linq;
using AMLRider.Library.Helpers;

namespace AMLRider.Library.Aml
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