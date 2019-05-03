using System;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Helpers;

namespace Iodd2AmlConverter.Library.Rules
{
    /// <inheritdoc />
    /// <summary>
    /// Responsible for the StdVariableRefRule.
    /// </summary>
    public class StdVariableRefRule : IConversionRule
    {
        /// <inheritdoc />
        /// <summary>
        /// Checks if the rule can be applied to the node.
        /// </summary>
        /// <param name="element">A given XML element.</param>
        /// <returns>True, if the rule can be applied. False, if not.</returns>
        public bool CanApplyRule(XElement element)
        {
            return element.Name.LocalName == "StdVariableRef";
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates the vendor logo tag.
        /// </summary>
        /// <param name="element">The given IODD element.</param>
        /// <returns>The created AML element.</returns>
        /// <exception cref="T:System.InvalidOperationException"></exception>
        public XElement Apply(XElement element)
        {
            if(!CanApplyRule(element))
                throw new InvalidOperationException("The given node is not a <StdVariableRef> node.");
            
            var stdSingleValueRef = GetStdSingleValueRef(element);
            return CreateStdVariableRefTag(stdSingleValueRef);
        }

        /// <summary>
        /// Gets the stdSingleValueRef.
        /// </summary>
        /// <param name="element">The given IODD element.</param>
        /// <returns>The stdSingleValueRef as a string.</returns>
        private static string GetStdSingleValueRef(XElement element)
        {
            var subElement = element.Element("StdSingleValueRef");
            var stdSingleValueRef = subElement.GetAttributeValue("value");

            return stdSingleValueRef;
        }

        /// <summary>
        /// Creates the StdVariableRefTag.
        /// </summary>
        /// <param name="stdSingleValueRef">The stdSingleValueRef as a string.</param>
        /// <returns>The created AML element.</returns>
        private static XElement CreateStdVariableRefTag(string stdSingleValueRef)
        {
            var internalElement = XmlHelper.CreateElement("InternalElement");
            internalElement.SetAttributeValue("Name", "V_SystemCommand");
            internalElement.SetAttributeValue("ID", "V_SystemCommand");
            var description = XmlHelper.CreateElement(internalElement, "Description", "Factory Reset");
            var attribute1 = XmlHelper.CreateElement(internalElement, "Attribute");
            attribute1.SetAttributeValue("Name", "Factory Reset");
            attribute1.SetAttributeValue("AttributeDataType", "xs:integer");
            var value1 = XmlHelper.CreateElement(attribute1, "Value", stdSingleValueRef);
            var attribute2 = XmlHelper.CreateElement(internalElement, "Attribute");
            attribute2.SetAttributeValue("Name", "ActionStartedMessage");
            attribute2.SetAttributeValue("AttributeDataType", "xs:string");
            var value2 = XmlHelper.CreateElement(attribute2, "Value", "Factory reset is now in progress!");

            return internalElement;
        }
    }
}