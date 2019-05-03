using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    public class TextRedefine : IoddElement
    {

        #region Attributes

        public string Id { get; set; }
        
        public string Value { get; set; }

        #endregion

        public override void Deserialize(XElement element)
        {
            Id = element.GetAttributeValue("id");
            Value = element.GetAttributeValue("value");
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
    }
}