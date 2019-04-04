using System.Collections.Generic;

namespace AMLRider.Library.Iodd
{
    
    public class Variable
    {

        #region Attributes

        public string Id { get; set; }
        
        public string AccessRights { get; set; }
        
        public bool Dynamic { get; set; }
        
        public bool ModifiesOtherVariables { get; set; }
        
        public bool ExcludedFromDataStorage { get; set; }
        
        public ushort Index { get; set; }

        #endregion

        #region Elements

        public List<DataType> DataTypes { get; set; }
        
        public List<DataTypeRef> DataTypeRefs { get; set; }
        
        public Name Name { get; set; }
        
        public Description Description { get; set; }

        #endregion
        
    }
    
}