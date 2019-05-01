using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;
using Attribute = AMLRider.Library.Aml.Attribute;

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
            var stdVariableRef = new InternalElement();
            stdVariableRef.Name = Id;
            stdVariableRef.Id = Id;
            var defaultValue = new Attribute();
            defaultValue.Value = new Value
            {
                Content = DefaultValue
            };
            var fixedLengthRestriction = new Attribute();
            fixedLengthRestriction.Value = new Value
            {
                Content = FixedLengthRestriction.ToString()
            };
            var excludeFromDataStorage = new Attribute();
            excludeFromDataStorage.Value = new Value
            {
                Content = ExcludeFromDataStorage.ToString()
            };
            stdVariableRef.Attributes.Add(defaultValue);
            stdVariableRef.Attributes.Add(fixedLengthRestriction);
            stdVariableRef.Attributes.Add(excludeFromDataStorage);
            foreach (var singleValueRef in SingleValueRefs)
            {
                var amlElement = singleValueRef.ToAml();
                stdVariableRef.Attributes.Add(amlElement as Attribute);
            }
            foreach (var singleValue in SingleValues)
            {
                var amlElement = singleValue.ToAml();
                stdVariableRef.Attributes.Add(amlElement as Attribute);
            }
            foreach (var valueRange in ValueRanges)
            {
                var amlElement = valueRange.ToAml();
                stdVariableRef.Attributes.Add(amlElement as Attribute);
            }
            if (RecordItemRef != null)
                stdVariableRef.Attributes.Add(RecordItemRef.ToAml() as Attribute);
            
            return stdVariableRef;
        }
    }
}