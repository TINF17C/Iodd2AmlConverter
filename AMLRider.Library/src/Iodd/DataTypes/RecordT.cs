using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.DataTypes
{
    public class RecordT: ComplexDataType
    {
        public int BitLength { get; set; }
        public List<RecordItem> RecordItems { get; set; }
        public override void Deserialize(XElement element)
        {
            base.Deserialize();
            BitLength = int.Parse(element.GetAttributeValue("bitLength"));
            foreach (var recordItem in element.SubElements("RecordItem"))
            {
                var recordItemVar = new RecordItem();
                recordItemVar.Deserialize(recordItem);
                RecordItems.Add(recordItemVar);
            }
            
        }

        public override AmlElement ToAml()
        {
            // TODO
            var internalElement = new InternalElement();

            return internalElement;
        }
    }
}