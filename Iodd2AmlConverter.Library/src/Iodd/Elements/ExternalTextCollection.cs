using System.Collections.Generic;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Extensions;

namespace Iodd2AmlConverter.Library.Iodd.Elements
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
            var element = new InternalElement
            {
                Name = "aml-text=TI_TextCollection"
            };

            var primaryLanguageAml = PrimaryLanguage.ToAml() as InternalElement;
            element.InternalElements.Add(primaryLanguageAml);

            foreach (var language in Languages)
            {
                var languageAml = language.ToAml() as InternalElement;
                element.InternalElements.Add(languageAml);
            }

            return element;
        }
    }
}