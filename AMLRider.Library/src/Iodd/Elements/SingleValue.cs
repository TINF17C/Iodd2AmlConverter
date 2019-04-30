using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{
    
    public class SingleValue : IoddElement
    {
        
        #region Attributes
        
        public string Value { get; set; }
        
        #endregion
        
        #region Elements
        
        [Optional]
        public Name Name { get; set; }
        
        #endregion

        public override void Deserialize(XElement element)
        {
            Value = element.GetAttributeValue("value");
            if (element.SubElement("Name") == null)
                return;
            
            Name = new Name();
            Name.Deserialize(element.SubElement("Name"));
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
    }
    
}