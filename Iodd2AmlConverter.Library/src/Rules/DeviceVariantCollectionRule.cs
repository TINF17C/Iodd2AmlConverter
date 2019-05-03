using Iodd2AmlConverter.Library.Rules;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Helpers;
using Iodd2AmlConverter.Library.Rules.DataObjects;


namespace Iodd2AmlConverter.Library.Rules
{
    public class DeviceVariantCollectionRule : IConversionRule
    {
       

        /// <inheritdoc />
        /// <summary>
        /// Checks if the rule can be applied to the node.
        /// </summary>
        /// <param name="element">A given XML element.</param>
        /// <returns>True, if the rule can be applied. False, if not.</returns>

        public bool CanApplyRule(XElement element)
        {
            return element.Name.LocalName == "DeviceVariantCollection";
        }
        
        
        /// <inheritdoc />
        /// <summary>
        /// Creates the vendor Features tag.
        /// </summary>
        /// <param name="element">The given IODD element.</param>
        /// <returns>The created AML element.</returns>
        
        public XElement Apply(XElement element)
        {

            if (!CanApplyRule(element))
                throw new InvalidOperationException("The given node is not a <DeviceVariantCollection> node.");

            var deviceVariant = GetDeviceVariant(element);
           

            return CreateDeviceVariantCollectionTag(deviceVariant);
        }

        /// <summary>
        /// Creates the AML DeviceVariantCollection Tag.
        /// </summary>
        private XElement CreateDeviceVariantCollectionTag(DeviceVariantCollectionObj obj)
        {
            var systemUnitClass = XmlHelper.CreateElement("SystemUnitClass ");
            systemUnitClass.SetAttributeValue("Name","DeviceVariant1");
            systemUnitClass.SetAttributeValue("ID",Guid.NewGuid().ToString());
            systemUnitClass.SetAttributeValue("RefBaseClassPath","AML-IODD Lib/Balluff-BNI_IOL-302-000-K006");
            systemUnitClass.Add(CreateAttribute("productID", "string",obj.ProductId));
            var descriptionElement = XmlHelper.CreateElement(systemUnitClass, "Description ");
            descriptionElement.SetAttributeValue("textId",obj.DescriptionText);
            var internalElement1=XmlHelper.CreateElement(systemUnitClass,"InternalElement ");
            internalElement1.SetAttributeValue("Name","deviceSymbol");
            internalElement1.SetAttributeValue("ID",Guid.NewGuid().ToString());
            var roleRequirements1 = XmlHelper.CreateElement(internalElement1, "RoleRequirements ");
            roleRequirements1.SetAttributeValue("RefBaseRoleClassPath","AutomationMLComponentStandardRCL/ComponentPicture");
            var externalInterface1 = XmlHelper.CreateElement(internalElement1, "ExternalInterface ");
            externalInterface1.SetAttributeValue("Name","ExternalDataReference");
            externalInterface1.SetAttributeValue("ID",Guid.NewGuid().ToString());
            externalInterface1.SetAttributeValue("RefBaseClassPath","AutomationMLInterfaceClassLib/AutomationMLBaseInterface/ExternalDataConnector");
            externalInterface1.Add(CreateAttribute("refURI","anyURI","/"+obj.DeviceSymbol));
            var internalElement2=XmlHelper.CreateElement(systemUnitClass,"InternalElement ");
            internalElement2.SetAttributeValue("Name","deviceIcon");
            internalElement2.SetAttributeValue("ID",Guid.NewGuid().ToString());
            var roleRequirements2 = XmlHelper.CreateElement(internalElement2, "RoleRequirements ");
            roleRequirements2.SetAttributeValue("RefBaseRoleClassPath","AutomationMLComponentStandardRCL/ComponentIcon");
            var externalInterface2 = XmlHelper.CreateElement(internalElement2, "ExternalInterface");
            externalInterface2.SetAttributeValue("Name","ExternalDataReference");
            externalInterface2.SetAttributeValue("ID",Guid.NewGuid().ToString());
            externalInterface2.SetAttributeValue("RefBaseClassPath","AutomationMLInterfaceClassLib/AutomationMLBaseInterface/ExternalDataConnector");
            externalInterface2.Add(CreateAttribute("refURI","anyURI","/"+obj.DeviceIcon));

            return systemUnitClass;
        }

       
        /// <summary>
        /// Gets the needed imformation out of the given IODD element.
        /// </summary>
        /// <param name="element">A given IODD element.</param>
        /// <returns>A DeviceVariant which contains the necessary information.</returns>

        private static DeviceVariantCollectionObj GetDeviceVariant(XElement element)
        {
            var devicevariantElement = element.Element("DeviceVariant ");
            var nameElement = element.Element("Name");
            var descriptionElement = devicevariantElement.Element("Description ");
            return new DeviceVariantCollectionObj
            {
                ProductId =devicevariantElement.GetAttributeValue("productId"),
                DeviceSymbol=devicevariantElement.GetAttributeValue("deviceSymbol"),
                DeviceIcon=devicevariantElement.GetAttributeValue("deviceIcon"),
                NameText=nameElement.GetAttributeValue("textId"),
                DescriptionText=descriptionElement.GetAttributeValue("textId")

            };
            
           
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
