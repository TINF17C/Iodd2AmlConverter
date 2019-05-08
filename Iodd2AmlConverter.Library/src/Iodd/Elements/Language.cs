using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;
using Iodd2AmlConverter.Library.Extensions;

namespace Iodd2AmlConverter.Library.Iodd.Elements
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

        public override AmlCollection ToAml()
        {
            var element = new InternalElement
            {
                Name = $"aml-lang={Lang}"
            };

            foreach (var text in Texts)
            {
                var amlElement = text.ToAml().Cast<Attribute>();
                element.Attributes.AddRange(amlElement);
            }

            return AmlCollection.Of(element);
        }

    }

}