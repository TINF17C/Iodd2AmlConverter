using System.Collections.Generic;

namespace AMLRider.Library.Iodd.DataTypes
{
    public class Float32T : DataType
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