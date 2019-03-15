using AMLRider.Library.Extensions;
using AMLRider.Library.Rules;
using System;
using System.Collections.Generic;
using System.Text;
using AMLRider.Library.Helpers;
using System.Xml.Linq;
using AMLRider.Library.Rules.DataObjects;
using AMLRider.Library.src.Rules.DataObjects;

namespace AMLRider.Library.Rules
{
    class FeaturesRule : IConversionRule
    {

       
        /// <inheritdoc />
        /// <summary>
        /// Checks if the rule can be applied to the node.
        /// </summary>
        /// <param name="element">A given XML element.</param>
        /// <returns>True, if the rule can be applied. False, if not.</returns>

        public bool CanApplyRule(XElement element)
        {
            return element.Name == "Features";
        }


        /// <inheritdoc />
        /// <summary>
        /// Creates the vendor Features tag.
        /// </summary>
        /// <param name="element">The given IODD element.</param>
        /// <returns>The created AML element.</returns>
        /// <exception cref="T:System.InvalidOperationException"></exception>
        public XElement Apply(XElement element)
        {

            if (!CanApplyRule(element))
                throw new InvalidOperationException("The given node is not a <Features> node.");

            var supportedAccessLocks = GetAccesLocks(element);
            return CreateFeaturesTag(supportedAccessLocks);
        }




        /// <summary>
        /// Gets the needed imformation out of the given IODD element.
        /// </summary>
        /// <param name="element">A given IODD element.</param>
        /// <returns>A Features which contains the necessary information.</returns>

        private static  Features GetAccesLocks(XElement element)
        {
            var supportedAccessLocksElement = element.Element("SupportedAccessLocks");
            return new Features
            {
                BlockParameter = element.GetAttributeValue("blockParameter"),
                DataStorageFeatures = element.GetAttributeValue("dataStorage"),
                Parameter = supportedAccessLocksElement.GetAttributeValue("parameter"),
                DataStorageAccessLocks = supportedAccessLocksElement.GetAttributeValue("dataStorage"),
                LocalUserInterface = supportedAccessLocksElement.GetAttributeValue("localUserInterface"),
                LocalParameterization= supportedAccessLocksElement.GetAttributeValue("localParameterization")

            };
            
        }

        /// <summary>
        /// Creates the AML FeaturesTag.
        /// </summary>
        
        private XElement CreateFeaturesTag(Features obj)
        {

            var internalElement = XmlHelper.CreateElement("InternalElement");
            internalElement.SetAttributeValue("Name", "Features");
            internalElement.SetAttributeValue("ID", "Features");
            internalElement.Add(CreateAttribute("blockParameter", "boolean",obj.BlockParameter));
            internalElement.Add(CreateAttribute("dataStorage", "boolean", obj.DataStorageFeatures));

            var internalElement_child = XmlHelper.CreateElement(internalElement, "InternalElement");
            internalElement_child.SetAttributeValue("Name", "SupportedAccessLocks");
            internalElement_child.SetAttributeValue("ID", Guid.NewGuid().ToString());

            internalElement_child.Add(CreateAttribute("parameter", "boolean",obj.Parameter));
            internalElement_child.Add(CreateAttribute("dataStorage", "boolean", obj.DataStorageAccessLocks));
            internalElement_child.Add(CreateAttribute("localUserInterface", "boolean", obj.LocalUserInterface));
            internalElement_child.Add(CreateAttribute("localParameterization", "boolean", obj.LocalParameterization));


            return null;
        }


        /// <summary>
        /// Helper method to create a simple attribute element.
        /// </summary>
        /// <param name="name">The value of the Attribute "Name"</param>
        /// <param name="dataType">The value of the Attribute "AttributeDataType"</param>
        /// <param name="value">The value of the "DefaultValue" element</param>
        /// <returns>A simple attribute element.</returns>
        private static XElement CreateAttribute(string name, string dataType, string value)
        {
            var attribute = XmlHelper.CreateElement("Attribute");
            attribute.SetAttributeValue("Name", name);
            attribute.SetAttributeValue("AttributeDataType", "xs:" + dataType);
            var valueTag = XmlHelper.CreateElement(attribute, "DefaultValue", value);

            return attribute;
        }
    }
}
