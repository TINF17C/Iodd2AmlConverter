namespace AMLRider.Library.Iodd
{
    
    public class RecordItemInfo
    {
        
        public int SubIndex { get; set; }
        
        public string DefaultValue { get; set; }
        
        public bool ModifiesOtherVariables { get; set; }
        
        public bool ExcludedFromDataStorage { get; set; }
        
    }
    
}