using System.Xml.Linq;

namespace AMLRider.Library.Rules.DataObjects
{
    public class ProcessDataCollectionObj
    {
        public  string DataId { get; set; }
        public  string DataInId { get; set; }

        public  int DataInBitLength { get; set; }

        public  string DatatypeRecordT { get; set; }
        public  int DatatypeBitLength { get; set; }
        public  int Subindex { get; set; }

        public  int BitOffset { get; set; }

        public  string DatatypeRefId { get; set; }
        public  string RecordItemTextId { get; set; }
        public  string DataInTextId { get; set; }



        public XElement[] ChildNodes { get; set; }
    }
}