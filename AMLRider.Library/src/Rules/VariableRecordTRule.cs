using System;
using System.Linq;
using System.Xml.Linq;
using AMLRider.Library.Extensions;
using AMLRider.Library.Helpers;
using AMLRider.Library.Rules.DataObjects;

namespace AMLRider.Library.Rules
{
    public class VariableRecordTRule : IConversionRule
    {
        public bool CanApplyRule(XElement element)
        {
            return element.Name == "Variable";
            // TODO: Change CanApplyRule for the different VariableTags
        }

        public XElement Apply(XElement element)
        {
            if(!CanApplyRule(element))
                throw new InvalidOperationException("The given node is not a <Variable>(RecordT) node.");
            
            var variableRecordTObj = GetVariableData(element);
            return CreateVariableRecordTTag(variableRecordTObj);
        }

        private static VariableRecordTObj GetVariableData(XElement element)
        {
            var datatype = element.Element("Datatype");
            var recordItem = datatype?.Element("RecordItem");
            var subNodes = element.Elements().ToArray();
            subNodes = subNodes.Skip(1).ToArray();
            
            var variableRecordTObj = new VariableRecordTObj
            {
                VariableId = element.GetAttributeValue("id"),
                Index = element.GetAttributeValue("index"),
                AccessRights = element.GetAttributeValue("accessRights"),
                BitLength = datatype?.GetAttributeValue("bitLength"),
                BitOffset = recordItem?.GetAttributeValue("bitOffset"),
                DescriptionId = element.Element("Description").GetAttributeValue("textId"),
                Name = element.Element("Name").GetAttributeValue("textId"),
                SubIndex = recordItem?.GetAttributeValue("subindex"),
                SubIndexAccessSupported = datatype?.GetAttributeValue("subindexAccessSupported"),
                DataTypeId = recordItem?.Element("DatatypeRef").GetAttributeValue("datatypeId"),
                RecordItemName = recordItem?.Element("Name").GetAttributeValue("textId"),
                VariableName = recordItem?.Element("Name").GetAttributeValue("textId"),
                ChildNodes = subNodes
            };

            return variableRecordTObj;
        }

        private static XElement CreateVariableRecordTTag(VariableRecordTObj variableRecordTObj)
        {
            var internalElement = XmlHelper.CreateElement("InternalElement");
            internalElement.SetAttributeValue("Name", variableRecordTObj.VariableId);
            internalElement.SetAttributeValue("ID", variableRecordTObj.VariableId);
            internalElement.Add(CreateAttribute("index", "integer", variableRecordTObj.Index));
            internalElement.Add(CreateAttribute("accessRights", "string", variableRecordTObj.AccessRights));
            internalElement.Add(CreateAttribute("bitLength", "integer", variableRecordTObj.BitLength));
            internalElement.Add(CreateAttribute("subindexAccessSupport", "boolean", variableRecordTObj.SubIndexAccessSupported));

            var internalElement2 = XmlHelper.CreateElement(internalElement, "InternalElement");
            internalElement2.SetAttributeValue("Name", variableRecordTObj.Name);
            internalElement2.SetAttributeValue("ID", Guid.NewGuid().ToString());
            var description = XmlHelper.CreateElement(internalElement2, "Description");
            description.SetAttributeValue("textId", variableRecordTObj.DescriptionId);
            var internalElement3 = XmlHelper.CreateElement(internalElement2, "InternalElement");
            internalElement3.SetAttributeValue("Name", variableRecordTObj.VariableName);
            internalElement3.SetAttributeValue("ID", Guid.NewGuid().ToString());
            internalElement3.Add(CreateAttribute("subindex", "integer", variableRecordTObj.SubIndex));
            internalElement3.Add(CreateAttribute("bitOffset", "integer", variableRecordTObj.BitOffset));
            var attribute = XmlHelper.CreateElement(internalElement3, "Attribute");
            attribute.SetAttributeValue("Name", variableRecordTObj.DataTypeId);
            var refSemantic = XmlHelper.CreateElement(attribute, "RefSemantic");
            refSemantic.SetAttributeValue("CorrespondingAttributePath", "ListType");
            var attribute1 = XmlHelper.CreateElement(attribute, "Attribute");
            attribute.SetAttributeValue("Name", "Input not inverted");
            attribute.SetAttributeValue("AttributeDataType", "xs:string");
            var attribute2 = XmlHelper.CreateElement(attribute, "Attribute");
            attribute2.SetAttributeValue("Name", "Input inverted");
            attribute2.SetAttributeValue("AttributeDataType", "xs:string");
            foreach (var childNode in variableRecordTObj.ChildNodes)
            {
                internalElement2.Add(childNode);
            }

            return internalElement;
        }

        private static XElement CreateAttribute(string name, string datatype, string value)
        {
            var attribute = XmlHelper.CreateElement("Attribute");
            attribute.SetAttributeValue("Name", name);
            attribute.SetAttributeValue("AttributeDataType", "xs:" + datatype);
            var attributeValue = XmlHelper.CreateElement(attribute, "Value", value);

            return attribute;
        }
    }
}