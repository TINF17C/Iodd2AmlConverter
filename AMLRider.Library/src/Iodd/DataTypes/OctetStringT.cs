using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.DataTypes
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