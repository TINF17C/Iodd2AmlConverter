using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Utils;

namespace AMLRider.Library.Iodd.DataTypes
{
    public class BooleanT : DataType
    {
        
        #region Attributes
        
        public Optional<SingleValue> Value { get; set; }

        #endregion
        
    }
}