using AMLRider.Library.Iodd.Elements;
using AMLRider.Library.Utils;

namespace AMLRider.Library.Iodd.DataTypes
{
    public class BooleanT : SimpleDataType
    {
        
        #region Attributes
        
        public Optional<SingleValue> Value { get; set; }

        #endregion
        
    }
}