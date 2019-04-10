using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;

namespace AMLRider.Library.Iodd.Elements
{
    public class ExternalTextCollection : IoddElement
    {
        
        public Language PrimaryLanguage { get; set; }
        
        public List<Language> Languages { get; set; }
        
        public override void Deserialize(XElement element)
        {
            throw new System.NotImplementedException();
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
    }
}