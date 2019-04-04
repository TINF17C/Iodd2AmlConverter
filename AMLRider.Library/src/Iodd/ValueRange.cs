using AMLRider.Library.Utils;

namespace AMLRider.Library.Iodd
{
    
    public class ValueRange
    {
        
        #region Attributes
        
        public int LowerValue { get; set; }
        
        public int UpperValue { get; set; }
        
        #endregion
        
        public Optional<Name> Name { get; set; }
        
    }
    
}