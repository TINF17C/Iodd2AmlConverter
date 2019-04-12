using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{
    public class StdVariableRef : IoddElement
    {
        #region Attributes

        public string Id { get; set; }

        [Optional]
        public string DefaultValue { get; set; }

        [Optional]
        public int FixedLengthRestriction { get; set; }

        [Optional]
        public bool ExcludeFromDataStorage { get; set; }

        #endregion

        #region Elements

        public List<StdSingleValueRef> SingleValueRefs { get; set; }

        public List<SingleValue> SingleValues { get; set; }

        public List<ValueRange> ValueRanges { get; set; }

        [Optional]
        public StdRecordItemRef RecordItemRef { get; set; }

        #endregion

        public override void Deserialize(XElement element)
        {
            Id = element.GetAttributeValue("id");
            if (element.HasAttribute("defaultValue"))
                DefaultValue = element.GetAttributeValue("defaultValue");

            if (element.HasAttribute("fixedLengthRestriction"))
                FixedLengthRestriction = int.Parse(element.GetAttributeValue("fixedLengthRestriction"));

            if (element.HasAttribute("excludeFromDataStorage"))
                ExcludeFromDataStorage = bool.Parse(element.GetAttributeValue("excludeFromDataStorage"));

            if (element.SubElement("StdSingleValueRef") != null)
            {
                SingleValueRefs = new List<StdSingleValueRef>();
                foreach (var singleValueRefElement in element.SubElements("StdSingleValueRef"))
                {
                    var singleValueRef = new StdSingleValueRef();
                    singleValueRef.Deserialize(singleValueRefElement);

                    SingleValueRefs.Add(singleValueRef);
                }
            }

            if (element.SubElement("SingleValue") != null)
            {
                SingleValues = new List<SingleValue>();
                foreach (var singleValueElement in element.SubElements("SingleValue"))
                {
                    var singleValue = new SingleValue();
                    singleValue.Deserialize(singleValueElement);

                    SingleValues.Add(singleValue);
                }
            }

            if (element.SubElement("ValueRange") != null)
            {
                ValueRanges = new List<ValueRange>();
                foreach (var valueRangeElement in element.SubElements("ValueRange"))
                {
                    var valueRange = new ValueRange();
                    valueRange.Deserialize(valueRangeElement);

                    ValueRanges.Add(valueRange);
                }
            }

            if (element.Element("StdRecordItemRef") == null)
                return;

            RecordItemRef = new StdRecordItemRef();
            RecordItemRef.Deserialize(element.SubElement("StdRecordItemRef"));
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
    }
}