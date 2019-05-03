using System.Xml.Linq;

namespace Iodd2AmlConverter.Library.Rules.DataObjects
{
    public class IoDeviceDataObj
    {
        public string SchemaInstance { get; set; }
        public XNamespace NameSpace { get; set; }
    }
}