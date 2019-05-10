using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Extensions;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    public class StdRecordItemRef : IoddElement
    {
        #region Attributes

        public int SubIndex { get; set; }

        [Optional]
        public string DefaultValue { get; set; }

        #endregion

        #region Elements

        public List<StdSingleValueRef> SingleValueRefs { get; set; }

        public List<SingleValue> SingleValues { get; set; }

        public List<ValueRange> ValueRanges { get; set; }

        #endregion

        public override void Deserialize(XElement element)
        {
            SubIndex = int.Parse(element.GetAttributeValue("subindex"));
            if (element.HasAttribute("defaultValue"))
                DefaultValue = element.GetAttributeValue("defaultValue");

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

            if (element.SubElement("ValueRange") == null) 
                return;
            
            ValueRanges = new List<ValueRange>();
            foreach (var valueRangeElement in element.SubElements("ValueRange"))
            {
                var valueRange = new ValueRange();
                valueRange.Deserialize(valueRangeElement);
                    
                ValueRanges.Add(valueRange);
            }
        }

        public override AmlCollection ToAml()
        {
            throw new NotImplementedException();
        }
        
    }
}