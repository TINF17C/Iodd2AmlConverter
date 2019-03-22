using System.Xml.Linq;
using System;
using System.Linq;
using AMLRider.Library.Helpers;
using AMLRider.Library.Extensions;
using AMLRider.Library.Rules.DataObjects;

namespace AMLRider.Library.Rules
{
    public class ProcessDataCollectionRule : IConversionRule
    {
        public bool CanApplyRule(XElement element)
        {
            return element.Name == "ProcessDataCollection";
        }

        public XElement Apply(XElement element)
        {
            if (!CanApplyRule(element))
                throw new InvalidOperationException("The given node is not a <ProcessDataCollection> node.");

            var processData = GetProcessData(element);

            return CreateProcessDataCollectionTag(processData);
        }

        private static XElement CreateProcessDataCollectionTag(ProcessDataCollectionObj obj)
        {
            var internalElement = XmlHelper.CreateElement("InternalElement");
            internalElement.SetAttributeValue("Name", obj.DataInId);
            internalElement.SetAttributeValue("id", obj.DataInId);
            
            internalElement.Add(CreateAttribute("bitLength", "integer", obj.DataInBitLength));
            
            var internalElement2 = XmlHelper.CreateElement(internalElement, "InternalElement");
            internalElement2.SetAttributeValue("Name", obj.DataInTextId);
            internalElement2.SetAttributeValue("ID", Guid.NewGuid().ToString());
            
            internalElement2.Add(CreateAttribute("bitLength", "integer", obj.DatatypeBitLength));
            
            var internalElement3 = XmlHelper.CreateElement(internalElement2, "InternalElement");
            internalElement3.SetAttributeValue("Name", obj.RecordItemTextId);
            internalElement3.SetAttributeValue("ID", Guid.NewGuid().ToString());
            
            internalElement3.Add(CreateAttribute("subindex", "integer", obj.SubIndex));
            internalElement3.Add(CreateAttribute("bitOffset", "integer", obj.BitOffset));
            
            var attribute1 = XmlHelper.CreateElement(internalElement3, "Attribute");
            attribute1.SetAttributeValue("Name", obj.DatatypeRefId);
            
            var refSemantic = XmlHelper.CreateElement(attribute1, "RefSemantic");
            refSemantic.SetAttributeValue("CorrespondingAttributePath", "ListType");
            
            // TODO: fix
            var attribute2 = XmlHelper.CreateElement(attribute1, "Attribute");
            attribute2.SetAttributeValue("Name", "false");
            attribute2.SetAttributeValue("AttributeDataType", "xs:boolean");

            var attribute3 = XmlHelper.CreateElement(attribute1, "Attribute");
            attribute3.SetAttributeValue("Name", "true");
            attribute3.SetAttributeValue("AttributeDataType", "xs:boolean");

            foreach (var childNode in obj.ChildNodes)
            {
                internalElement2.Add(childNode);
            }

            return internalElement;
        }

        private static ProcessDataCollectionObj GetProcessData(XContainer element)
        {
            var processData = element.Element("ProcessData");
            var subNodes = processData?.Elements().ToArray();
            subNodes = subNodes?.Skip(1).ToArray();

            var processDataIn = processData?.Element("ProcessDataIn");
            var datatype = processDataIn?.Element("Datatype");
            var recordItem = datatype?.Element("RecordItem");
            var datatypeRef = recordItem?.Element("DatatypeRef");
            var name1 = recordItem?.Element("Name");
            var name2 = processDataIn?.Element("Name");
            return new ProcessDataCollectionObj
            {
                DataId = processData.GetAttributeValue("id"),
                DataInId = processDataIn.GetAttributeValue("id"),
                DataInBitLength = processDataIn.GetAttributeValue("bitLength"),
                DatatypeRecordT = datatype.GetAttributeValue("xsi:type"),
                DatatypeBitLength = datatype.GetAttributeValue("bitLength"),
                SubIndex = recordItem.GetAttributeValue("subindex"),
                BitOffset = recordItem.GetAttributeValue("bitOffset"),
                DatatypeRefId = datatypeRef.GetAttributeValue("datatypeId"),
                RecordItemTextId = name1.GetAttributeValue("textId"),
                DataInTextId = name2.GetAttributeValue("textId"),
                ChildNodes = subNodes
            };
        }

        /// <summary>
        /// Helper method to create a simple attribute element.
        /// </summary>
        /// <param name="name">The value of the Attribute "Name"</param>
        /// <param name="dataType">The value of the Attribute "AttributeDataType"</param>
        /// <param name="value">The value of the "Value" element</param>
        /// <returns>A simple attribute element.</returns>
        private static XElement CreateAttribute(string name, string dataType, string value)
        {
            var attribute = XmlHelper.CreateElement("Attribute");
            attribute.SetAttributeValue("Name", name);
            attribute.SetAttributeValue("AttributeDataType", "xs:" + dataType);
            
            XmlHelper.CreateElement(attribute, "Value", value);
            return attribute;
        }
    }
}