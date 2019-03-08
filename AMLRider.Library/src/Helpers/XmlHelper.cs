using System.Xml.Linq;

namespace AMLRider.Library.Helpers
{
    
    public static class XmlHelper
    {

        public static XElement CreateElementWithValue(string element, object value)
        {
            var xElement= new XElement(element);
            xElement.SetValue(value);

            return xElement;
        }
        
    }
    
}