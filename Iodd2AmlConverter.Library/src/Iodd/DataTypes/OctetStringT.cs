using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;

namespace Iodd2AmlConverter.Library.Iodd.DataTypes
{
    public class OctetStringT : SimpleDataType
    {
        
        #region Attributes

        public int FixedLength { get; set; }
        
        #endregion

        public override void Deserialize(XElement element)
        {
            FixedLength = int.Parse(element.GetAttributeValue("fixedLength"));
        }

        public override AmlElement ToAml()
        {
            return base.ToAml();
        }

    }
}