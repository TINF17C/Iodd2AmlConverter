using System.Collections.Generic;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;
using Iodd2AmlConverter.Library.Extensions;

namespace Iodd2AmlConverter.Library.Iodd.DataTypes
{
    public class RecordT: ComplexDataType
    {
        public int BitLength { get; set; }
        public List<RecordItem> RecordItems { get; set; }
        public override void Deserialize(XElement element)
        {
            base.Deserialize(element);
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