using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;
using AMLRider.Library.Iodd.Elements;

namespace AMLRider.Library.Iodd.DataTypes
{
    public class UIntegerT : SimpleDataType
    {

        #region Attributes

        public int BitLength { get; set; }

        #endregion
        
        #region Elements

        public List<SingleValue> SingleValues { get; set; }
        
        public List<ValueRange> ValueRanges { get; set; }

        #endregion

        public UIntegerT()
        {
            SingleValues = new List<SingleValue>();
            ValueRanges = new List<ValueRange>();
        }

        public override void Deserialize(XElement element)
        {
            BitLength = int.Parse(element.GetAttributeValue("bitLength"));
            
            foreach (var subElement in element.SubElements("SingleValue"))
            {
                var singleValue = new SingleValue();
                singleValue.Deserialize(subElement);
            }
            
            foreach (var subElement in element.SubElements("ValueRange"))
            {
                var valueRange = new ValueRange();
                valueRange.Deserialize(subElement);
            }
        }

        public override AmlElement ToAml()
        {
            return base.ToAml();
        }

    }
}