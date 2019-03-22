using System.Xml.Linq;

namespace AMLRider.Library.Rules.DataObjects
{
    public class ProcessDataCollectionObj
    {
        
        public  string DataId { get; set; }
        
        public  string DataInId { get; set; }

        public  string DataInBitLength { get; set; }

        public  string DatatypeRecordT { get; set; }
        
        public  string DatatypeBitLength { get; set; }
        
        public  string SubIndex { get; set; }

        public  string BitOffset { get; set; }

        public  string DatatypeRefId { get; set; }
        
        public  string RecordItemTextId { get; set; }
        
        public  string DataInTextId { get; set; }
        
        public XElement[] ChildNodes { get; set; }
    }
}