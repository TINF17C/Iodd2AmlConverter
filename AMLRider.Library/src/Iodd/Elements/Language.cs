using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;
using Attribute = AMLRider.Library.Aml.Attribute;

namespace AMLRider.Library.Iodd.Elements
{

    public class Language : IoddElement
    {

        #region Attributes

        public string Lang { get; set; }

        #endregion

        #region Elements

        public List<Text> Texts { get; set; }

        public List<TextRedefine> TextRedefines { get; set; }

        #endregion

        public Language()
        {
            Texts = new List<Text>();
            TextRedefines = new List<TextRedefine>();
        }

        public override void Deserialize(XElement element)
        {
            Lang = element.GetAttributeValue("lang");

            foreach (var textElement in element.SubElements("Text"))
            {
                var text = new Text();
                text.Deserialize(textElement);

                Texts.Add(text);
            }

            foreach (var textElement in element.SubElements("TextRedefine"))
            {
                var text = new TextRedefine();
                text.Deserialize(textElement);

                TextRedefines.Add(text);
            }
        }

        public override AmlElement ToAml()
        {
            var element = new InternalElement
            {
                Name = $"aml-lang={Lang}"
            };

            foreach (var text in Texts)
            {
                var amlElement = text.ToAml() as Attribute;
                element.Attributes.Add(amlElement);
            }

            return element;
        }

    }

}