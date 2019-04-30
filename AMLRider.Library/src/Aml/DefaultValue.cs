using System.Xml.Linq;
using AMLRider.Library.Helpers;

namespace AMLRider.Library.Aml
{

    public class DefaultValue : AmlElement
    {

        public string Content { get; set; }

        public override XElement Serialize()
        {
            return XmlHelper.CreateElement("DefaultValue", Content ?? string.Empty);
        }

    }

}