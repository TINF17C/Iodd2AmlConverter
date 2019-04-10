using AMLRider.Library.Iodd.Elements;


namespace AMLRider.Library.Iodd.DataTypes
{
    public class BooleanT : SimpleDataType
    {
        
        #region Attributes
        
        [Optional]
        public SingleValue Value { get; set; }

        #endregion
        
    }
}