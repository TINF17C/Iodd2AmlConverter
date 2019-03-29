using AMLRider.Library.Extensions;
using AMLRider.Library.Helpers;
using AMLRider.Library.Rules;
using AMLRider.Library.src.Rules.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AMLRider.Library.Rules
{
    public class VariableVSnRwRule : IConversionRule
    {
        /// <inheritdoc />
        /// <summary>
        /// Checks if the rule can be applied.
        /// </summary>
        /// <param name="element">The given IODD.</param>
        /// <returns>True, if the rule can be applied. False if not.</returns>
        public bool CanApplyRule(XElement element)
        {
            return element.Name.LocalName == "Variable";
            // TODO: Identify which variable rule
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates the variable V_SN_RW tag.
        /// </summary>
        /// <param name="element">The given IODD element.</param>
        /// <returns>The created AML element.</returns>
        public XElement Apply(XElement element)
        {
            if(!CanApplyRule(element))
                throw new InvalidOperationException("The given node is not a <Variable> node.");
            
            var variableData = GetVariableData(element);
            
            return CreateVariableTag(variableData);
        }

        /// <summary>
        /// Gets the needed information out of the given IODD element.
        /// </summary>
        /// <param name="element">A given IODD element.</param>
        /// <returns>A Variable V_SN_RW which contains the necessary information.</returns>
        private static VariableV_SN_RWObj GetVariableData(XElement element)
        {
            var fixedLength = element.Element("Datatype").GetAttributeValue("fixedLength");
            var encodingType = element.Element("Datatype").GetAttributeValue("encoding");
            var nameTextId = element.Element("Name").GetAttributeValue("textId");
            var descriptionTextId = element.Element("Description").GetAttributeValue("textId");
            
            var variableObj = new VariableV_SN_RWObj
            {
                VariableId = element.GetAttributeValue("id"),
                VariableIndex = element.GetAttributeValue("index"),
                AccessRights = element.GetAttributeValue("accessRights"),
                FixedLength = fixedLength,
                EncodingType = encodingType,
                NameTextId = nameTextId,
                DescriptionTextId = descriptionTextId
            };

            return variableObj;
        }


        /// <summary>
        /// Creates the VariableVSnRwRule element.
        /// </summary>
        /// <param name="obj">A variableVSnRw object</param>
        /// <returns>The created AML element.</returns>
        private static XElement CreateVariableTag(VariableV_SN_RWObj obj)
        {
            var internalElement = XmlHelper.CreateElement("InternalElement");
            internalElement.SetAttributeValue("Name", obj.VariableId);
            internalElement.SetAttributeValue("ID", obj.VariableId);
            internalElement.Add(CreateAttribute("index", "integer",obj.VariableIndex));
            internalElement.Add(CreateAttribute("accessRights", "string", obj.AccessRights));
            internalElement.Add(CreateAttribute("fixedLength", "integer", obj.FixedLength));
            internalElement.Add(CreateAttribute("encoding", "string", obj.EncodingType));
            var attribute = XmlHelper.CreateElement(internalElement, "Attribute");
            attribute.SetAttributeValue("Name", obj.NameTextId);
            attribute.SetAttributeValue("AttributeDataType", "xs:string");
            var description = XmlHelper.CreateElement(internalElement, "Description");
            description.SetAttributeValue("textId", obj.DescriptionTextId);

            return internalElement;
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
            var valueTag = XmlHelper.CreateElement(attribute, "Value", value);

            return attribute;
        }
    }
}
