using System;
using System.Linq;
using System.Xml.Linq;
using AMLRider.Library.Extensions;
using AMLRider.Library.Helpers;

namespace AMLRider.Library.Rules
{
    public class VendorLogoRule : IConversionRule 
    {
        public bool CanApplyRule(XElement element)
        {
            return element.Name == "VendorLogo";
        }

        public XElement Apply(XElement element)
        {
            if(!CanApplyRule(element))
                throw new InvalidOperationException("The given node is not a <VendorLogo> node.");
            
            var vendorLogo = GetDocumentInfo(element);
            return CreateVendorLogoTag(vendorLogo);
        }
        
        private static string GetDocumentInfo(XElement element)
        {
            var vendorLogo = element.GetAttributeValue("name");
            return vendorLogo;
        }
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