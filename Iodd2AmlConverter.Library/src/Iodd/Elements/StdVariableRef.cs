using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;
using Attribute = Iodd2AmlConverter.Library.Aml.Elements.Attribute;

namespace Iodd2AmlConverter.Library.Iodd.Elements
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

        public override AmlCollection ToAml()
        {
            var stdVariableRef = new InternalElement
            {
                Name = Id ?? string.Empty,
                Id = Id
            };

            if (DefaultValue != null)
            {
                var defaultValue = new Attribute
                {
                    Name = "defaultValue",
                    AttributeDataType = "xs:integer",
                    Value = new Value
                    {
                        Content = DefaultValue
                    }
                };
                
                stdVariableRef.Attributes.Add(defaultValue);
            }
            
            var fixedLengthRestriction = new Attribute
            {
                Name = "fixedLengthRestriction",
                AttributeDataType = "xs:integer",
                Value = new Value
                {
                    Content = FixedLengthRestriction.ToString()
                }
            };

            var excludeFromDataStorage = new Attribute
            {
                Name = "excludeFromDataStorage",
                AttributeDataType = "xs:boolean",
                Value = new Value
                {
                    Content = ExcludeFromDataStorage.ToString()
                }
            };
            
            stdVariableRef.Attributes.Add(fixedLengthRestriction);
            stdVariableRef.Attributes.Add(excludeFromDataStorage);

            if (SingleValueRefs != null)
            {
                foreach (var singleValueRef in SingleValueRefs)
                {
                    var amlElement = singleValueRef.ToAml();
                    stdVariableRef.Attributes.AddRange(amlElement.Cast<Attribute>());
                }
            }

            if (SingleValues != null)
            {
                foreach (var singleValue in SingleValues)
                {
                    var amlElement = singleValue.ToAml();
                    stdVariableRef.Attributes.AddRange(amlElement.Cast<Attribute>());
                }
            }

            if (ValueRanges != null)
            {
                foreach (var valueRange in ValueRanges)
                {
                    var amlElement = valueRange.ToAml();
                    stdVariableRef.Attributes.AddRange(amlElement.Cast<Attribute>());
                }
            }

            if (RecordItemRef != null)
                stdVariableRef.Attributes.AddRange(RecordItemRef.ToAml().Cast<Attribute>());

            return AmlCollection.Of(stdVariableRef);
        }

    }

}