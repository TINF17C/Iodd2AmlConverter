using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{
    public class ExternalTextCollection : IoddElement
    {
        
        public Language PrimaryLanguage { get; set; }
        
        [Optional]
        public List<Language> Languages { get; set; }

        public ExternalTextCollection()
        {
            Languages = new List<Language>();
        }
        
        public override void Deserialize(XElement element)
        {
            PrimaryLanguage = new Language();
            PrimaryLanguage.Deserialize(element.SubElement("PrimaryLanguage"));

            foreach (var languageElement in element.SubElements("Language"))
            {
                var language = new Language();
                language.Deserialize(languageElement);
                
                Languages.Add(language);
            }
        }

        public override AmlElement ToAml()
        {
            return new InternalElement
            {
                Name = "ExternalTextCollection",
                Id = "ExternalTextCollection"
            };
        }
    }
}