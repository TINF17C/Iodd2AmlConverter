using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;
using AMLRider.Library.Iodd.DataTypes;
using Attribute = AMLRider.Library.Aml.Attribute;

namespace AMLRider.Library.Iodd.Elements
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

        public override AmlElement ToAml()
        {
            var element = new InternalElement
            {
                Name = Id,
                Id = Id
            };

            element.Attributes.Add(CreateAttribute("index", "xs:integer", Index.ToString()));
            element.Attributes.Add(CreateAttribute("accessRights", "xs:string", AccessRights ?? string.Empty));

            if (DataType != null)
                element.Attributes.Add(DataType.ToAml() as Attribute);

            if (Name != null)
                element.AmlName = new AmlName {Content = Name.TextId};

            if (Description != null)
                element.AmlDescription = new AmlDescription {Content = Description.TextId};

            return element;
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