using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

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
            throw new System.NotImplementedException();
        }
    }
}