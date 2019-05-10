using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Iodd.DataTypes;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{

    public class Variable : IoddElement
    {

        #region Attributes

        public string Id { get; set; }

        public string AccessRights { get; set; }

        [Optional]
        public bool? Dynamic { get; set; }

        [Optional]
        public bool? ModifiesOtherVariables { get; set; }

        [Optional]
        public bool? ExcludedFromDataStorage { get; set; }

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
            Id = element.GetAttributeValue("id");
            AccessRights = element.GetAttributeValue("accessRights");

            if (element.HasAttribute("dynamic"))
                Dynamic = bool.Parse(element.GetAttributeValue("dynamic"));

            if (element.HasAttribute("modifiesOtherVariables"))
                ModifiesOtherVariables = bool.Parse(element.GetAttributeValue("modifiesOtherVariables"));

            if (element.HasAttribute("excludedFromDataStorage"))
                ExcludedFromDataStorage = bool.Parse(element.GetAttributeValue("excludedFromDataStorage"));

            if (element.HasAttribute("index"))
                Index = ushort.Parse(element.GetAttributeValue("index"));

            if (element.HasAttribute("defaultValue"))
                DefaultValue = element.GetAttributeValue("defaultValue");


            if (element.SubElement("Datatype") != null)
            {
                var subElement = element.SubElement("Datatype");

                var id = subElement.Attribute(subElement.GetNamespaceOfPrefix("xsi") + "type")?.Value;
                DataType = DataType.CreateDataTypeBasedOnId(id, subElement);
            }

            if (element.SubElement("RecordItemInfo") != null)
            {
                RecordItemInfo = new RecordItemInfo();
                RecordItemInfo.Deserialize(element.SubElement("RecordItemInfo"));
            }

            if (element.SubElement("Name") != null)
            {
                Name = new Name();
                Name.Deserialize(element.SubElement("Name"));
            }

            if (element.SubElement("Description") == null)
                return;

            Description = new Description();
            Description.Deserialize(element.SubElement("Description"));
        }

        public override AmlCollection ToAml()
        {
            var element = new InternalElement
            {
                Name = Id
            };

            element.Attributes.Add(CreateAttribute("Index", "xs:integer", Index.ToString()));
            element.Attributes.Add(CreateAttribute("AccessRights", "xs:string", AccessRights ?? string.Empty));
            element.Attributes.Add(CreateAttribute(Name?.TextId ?? string.Empty, "xs:string", null));
            element.Attributes.Add(CreateAttribute("Description", "xs:string", Description?.TextId ?? string.Empty));

            if (DataType != null)
                element.Attributes.Add(DataType.ToAml() as Attribute);

            return AmlCollection.Of(element);
        }

        private static Attribute CreateAttribute(string name, string type, string value)
        {
            return new Attribute
            {
                Name = name,
                AttributeDataType = type,
                Value = new Value
                {
                    Content = value
                }
            };
        }

    }

}