using System.Collections.Generic;
using System.Xml.Linq;

namespace AMLRider.Library.Extensions
{
    
    public static class XElementExtensions
    {

        public static string GetAttributeValue(this XElement element, string attributeName)
        {
            var attribute = element.Attribute(attributeName);
            return attribute?.Value;
        }

        public static bool HasAttribute(this XElement element, string attributeName)
        {
            return element.Attribute(attributeName) != null;
        }

        public static XElement SubElement(this XElement element, string elementName)
        {
            return element.Element(element.GetDefaultNamespace() + elementName);
        }
        
        public static IEnumerable<XElement> SubElements(this XElement element, string elementName)
        {
            return element.Elements(element.GetDefaultNamespace() + elementName);
        }
        
    }
    
}