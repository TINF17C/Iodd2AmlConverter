namespace Iodd2AmlConverter.Library.Iodd.DataTypes
{
    
    public enum DataTypeNames
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