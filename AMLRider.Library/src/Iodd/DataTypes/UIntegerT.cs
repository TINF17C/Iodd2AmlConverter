using System.Collections.Generic;
using AMLRider.Library.Iodd.Elements;

namespace AMLRider.Library.Iodd.DataTypes
{
    public class UIntegerT : SimpleDataType
    {

        #region Attributes

        public int BitLength { get; set; }

        #endregion
        
        #region Elements

        public List<SingleValue> SingleValues { get; set; }
        
        public List<ValueRange> ValueRanges { get; set; }

        #endregion

        public UIntegerT()
        {
            SingleValues = new List<SingleValue>();
            ValueRanges = new List<ValueRange>();
        }
        
    }
}