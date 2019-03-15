using System.Xml.Linq;

namespace AMLRider.Library.Rules.DataObjects
{
    public class VariableRecordTObj
    {
        public string VariableId { get; set; }
        public string Index { get; set; }
        public string SubIndexAccessSupported { get; set; }
        public string SubIndex { get; set; }
        public string AccessRights { get; set; }
        public string BitLength { get; set; }
        public string BitOffset { get; set; }
        public string DataTypeId { get; set; }
        public string Name { get; set; }
        public string VariableName { get; set; }
        public string DescriptionId { get; set; }
        public XElement[] ChildNodes { get; set; }
        public string RecordItemName { get; set; }
    }
}