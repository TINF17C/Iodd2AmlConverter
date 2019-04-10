using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;

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

        public StdVariableRef()
        {
        }

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