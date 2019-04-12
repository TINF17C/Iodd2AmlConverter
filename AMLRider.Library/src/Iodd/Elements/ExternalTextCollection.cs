using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;

namespace AMLRider.Library.Iodd.Elements
{
    public class ExternalTextCollection : IoddElement
    {
        
        public Language PrimaryLanguage { get; set; }
        
        [Optional]
        public List<Language> Languages { get; set; }
        
        public override void Deserialize(XElement element)
        {
            PrimaryLanguage = new Language();
            PrimaryLanguage.Deserialize(element.Element("PrimaryLanguage"));

            foreach (var languageElement in element.Elements("Language"))
            {
                var language = new Language();
                language.Deserialize(languageElement);
                
                Languages.Add(language);
            }
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
    }
}