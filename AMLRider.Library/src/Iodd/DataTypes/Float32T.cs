using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;
using AMLRider.Library.Iodd.Elements;

namespace AMLRider.Library.Iodd.DataTypes
{

    public class Float32T : SimpleDataType
    {

        #region Elements

        public List<SingleValue> SingleValues { get; set; }

        public List<ValueRange> ValueRanges { get; set; }

        #endregion

        public Float32T()
        {
            SingleValues = new List<SingleValue>();
            ValueRanges = new List<ValueRange>();
        }

        public override void Deserialize(XElement element)
        {
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
            var attribute = new Attribute
            {
                Name = Id,
                AttributeDataType = "xs:float"
            };

            foreach (var singleValue in SingleValues)
            {
                var amlElement = singleValue.ToAml();
                
            }

            foreach (var valueRange in ValueRanges)
            {
                var amlElement = valueRange.ToAml();
            }

            return attribute;
        }

    }

}