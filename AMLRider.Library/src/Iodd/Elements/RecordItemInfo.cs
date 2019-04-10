using System.Xml.Linq;
using AMLRider.Library.Aml;

namespace AMLRider.Library.Iodd.Elements
{
    
    public class RecordItemInfo : IoddElement
    {
        
        public int SubIndex { get; set; }
        
        [Optional]
        public string DefaultValue { get; set; }
        
        [Optional]
        public bool ModifiesOtherVariables { get; set; }
        
        [Optional]
        public bool ExcludedFromDataStorage { get; set; }

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