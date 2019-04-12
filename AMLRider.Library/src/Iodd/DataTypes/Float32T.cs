using System.Collections.Generic;
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
        
    }
}