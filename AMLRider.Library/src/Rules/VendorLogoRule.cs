using System;
using System.Linq;
using System.Xml.Linq;
using AMLRider.Library.Extensions;
using AMLRider.Library.Helpers;

namespace AMLRider.Library.Rules
{
    public class VendorLogoRule : IConversionRule 
    {
        /// <summary>
        /// Checks if the rule can be applied to the node.
        /// </summary>
        /// <param name="element">A given XML element.</param>
        /// <returns>True, if the rule can be applied. False, if not.</returns>
        public bool CanApplyRule(XElement element)
        {
            return element.Name == "VendorLogo";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
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