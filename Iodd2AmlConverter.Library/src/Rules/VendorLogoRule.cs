using System;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Helpers;

namespace Iodd2AmlConverter.Library.Rules
{
    /// <inheritdoc />
    /// <summary>
    /// Responsible for the conversion of the vendor logo.
    /// </summary>
    public class VendorLogoRule : IConversionRule 
    {
        /// <inheritdoc />
        /// <summary>
        /// Checks if the rule can be applied to the node.
        /// </summary>
        /// <param name="element">A given XML element.</param>
        /// <returns>True, if the rule can be applied. False, if not.</returns>
        public bool CanApplyRule(XElement element)
        {
            return element.Name.LocalName == "VendorLogo";
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
                throw new InvalidOperationException("The given node is not a <VendorLogo> node.");
            
            var vendorLogo = GetVendorLogo(element);
            return CreateVendorLogoTag(vendorLogo);
        }
        
        /// <summary>
        /// Gets the vendor logo from the IODD file.
        /// </summary>
        /// <param name="element">The XML element of the IODD file.</param>
        /// <returns>The name of the vendor logo as a string.</returns>
        private static string GetVendorLogo(XElement element)
        {
            var vendorLogo = element.GetAttributeValue("name");
            return vendorLogo;
        }
        
        /// <summary>
        /// Creates the AML section for the vendor logo.
        /// </summary>
        /// <param name="vendorLogo">The name of the vendor logo as a string.</param>
        /// <returns>The AML element which is created out of the IODD section.</returns>
        private static XElement CreateVendorLogoTag(string vendorLogo)
        {
            var internalElement = XmlHelper.CreateElement("InternalElement");
            internalElement.SetAttributeValue("Name", "ManufacturerIcon");
            internalElement.SetAttributeValue("ID", "GUID");
            var externalInterface = XmlHelper.CreateElement(internalElement, "ExternalInterface");
            externalInterface.SetAttributeValue("Name", "ExternalDataConnector");
            externalInterface.SetAttributeValue("ID", "GUID");
            externalInterface.SetAttributeValue("RefBaseClassPath", "AutomationMLInterfaceClassLib/AutomationMLBaseInterface/ExternalDataConnector");
            var supportedRoleClass = XmlHelper.CreateElement(internalElement, "SupportedRoleClass");
            supportedRoleClass.SetAttributeValue("RefRoleClassPath", "AutomationMLComponentStandardRCL/ManufacturerIcon");
            var attribute = XmlHelper.CreateElement(externalInterface, "Attribute");
            attribute.SetAttributeValue("Name", "refURI");
            attribute.SetAttributeValue("AttributeDataType", "xs:anyURI");
            var value = XmlHelper.CreateElement(attribute, "Value", vendorLogo);

           
            return internalElement;
        }
    }
}