using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Iodd.DataTypes;

namespace AMLRider.Library.Iodd.Elements
{
    
    public class Variable : IoddElement
    {

        #region Attributes

        public string Id { get; set; }
        
        public string AccessRights { get; set; }
        
        [Optional]
        public bool Dynamic { get; set; }
        
        [Optional]
        public bool ModifiesOtherVariables { get; set; }
        
        [Optional]
        public bool ExcludedFromDataStorage { get; set; }
        
        public ushort Index { get; set; }
        
        [Optional]
        public string DefaultValue { get; set; }

        #endregion

        #region Elements

        public DataType DataType { get; set; }
        
        public DataTypeRef DataTypeRef { get; set; }
        
        [Optional]
        public RecordItemInfo RecordItemInfo { get; set; }
        
        public Name Name { get; set; }
        
        [Optional]
        public Description Description { get; set; }

        #endregion

        public override void Deserialize(XElement element)
        {
            throw new System.NotImplementedException();
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
    }
    
}