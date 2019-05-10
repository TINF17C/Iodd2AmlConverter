using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Iodd.Elements;

namespace Iodd2AmlConverter.Library.Iodd.DataTypes
{

    public class BooleanT : SimpleDataType
    {

        #region Attributes

        [Optional]
        public SingleValue Value { get; set; }

        #endregion

        public override void Deserialize(XElement element)
        {
            if (element.HasAttribute("value"))
            {
                Value = new SingleValue();
                Value.Deserialize(element);
            }
        }

        public override AmlElement ToAml()
        {
            return new Attribute
            {
                Name = Id,
                AttributeDataType = "xs:boolean",
                Value = new Value
                {
                    Content = Value?.Value
                }
            };
        }

    }

}