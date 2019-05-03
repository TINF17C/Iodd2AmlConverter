using System.Collections.Generic;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;

namespace Iodd2AmlConverter.Library.Iodd.DataTypes
{
    public class RecordItem : ComplexDataType
    {
        public int SubIndex { get; set; }
        public int BitOffset { get; set; }
        public string AccessRightRestriction { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DataTypeRef> DataTypeRefs { get; set; }
        public List<SimpleDataType> SimpleDataTypes { get; set; }
        public override void Deserialize(XElement element)
        {
            SubIndex = int.Parse(element.GetAttributeValue("subindex"));
            BitOffset = int.Parse(element.GetAttributeValue("bitOffset"));
            AccessRightRestriction = element.GetAttributeValue("accessRightRestriction");
            Name = element.SubElement("Name").GetAttributeValue("textId");
            
            if (element.SubElement("Description") != null)
                Description = element.SubElement("Description").GetAttributeValue("textId");
            
            foreach (var dataTypeRef in element.SubElements("DataTypeRef"))
            {
                var dataTypeRefVar = new DataTypeRef();
                dataTypeRefVar.Deserialize(dataTypeRef);
                DataTypeRefs.Add(dataTypeRefVar);
            }

            foreach (var simpleDataType in element.SubElements("SimpleDataType"))
            {
                var simpleDataTypeVar = new SimpleDataType();
                simpleDataTypeVar.Deserialize(simpleDataType);
                SimpleDataTypes.Add(simpleDataTypeVar);
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