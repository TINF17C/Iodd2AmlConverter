using System.Xml.Linq;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd
{
    public class Description : IIoddElement
    {
        
        public string TextId { get; set; }

        public void Deserialize(XElement element)
        {
            TextId = element.GetAttributeValue("textId");
        }
        
    }
}