using System.Xml.Linq;

namespace AMLRider.Library.Helpers
{
    
    public static class XmlHelper
    {

        public static XElement CreateElement(string element)
        {
            return new XElement(element);
        }
        
        public static XElement CreateElement(string element, object value)
        {
            var xElement= new XElement(element);
            xElement.SetValue(value);

            return xElement;
        }

        public static XElement CreateElement(XElement parent, string element)
        {
            var xElement = CreateElement(element);
            parent.Add(xElement);

            return xElement;
        }

        public static XElement CreateElement(XElement parent, string element, string value)
        {
            var xElement = CreateElement(element, value);
            parent.Add(xElement);

            return xElement;
        }
        
    }
    
}