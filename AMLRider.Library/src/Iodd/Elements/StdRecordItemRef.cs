using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;

namespace AMLRider.Library.Iodd.Elements
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
            throw new System.NotImplementedException();
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
        
    }
}