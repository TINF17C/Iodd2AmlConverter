namespace AMLRider.Library.Iodd
{
    
    public enum DataTypes
    {
        
        #region SimpleDataTypes
        
        BooleanT,
        NumberT,
        UIntegerT,
        IntegerT,
        Float32T,
        StringT,
        OctetStringT,
        TimeT,
        TimeSpanT,
        
        #endregion
        
        #region ComplexDataTypes
        
        ArrayT,
        RecordT,
        
        #endregion
        
        #region ProcessDataUnionTypes
        
        ProcessDataInUnionT,
        ProcessDataOutUnionT
        
        #endregion
        
    }
    
}