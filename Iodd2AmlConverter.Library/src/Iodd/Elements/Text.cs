using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Extensions;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    public class Text : IoddElement
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
            return new Attribute
            {
                Name = Id,
                Value = new Value
                {
                    Content = Value
                }
            };
        }
    }
}