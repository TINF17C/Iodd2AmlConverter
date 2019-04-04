using System;
using System.Collections.Generic;

namespace AMLRider.Library.Iodd
{
    public class StdRecordItemRef
    {

        #region Attributes

        public string DefaultValue { get; set; }
        
        public int SubIndex { get; set; }

        #endregion

        #region Elements

        public List<StdSingleValueRef> SingleValueRefs { get; set; }
        
        public List<SingleValue> SingleValues { get; set; }
        
        public List<ValueRange> ValueRanges { get; set; }

        #endregion
        
    }
}