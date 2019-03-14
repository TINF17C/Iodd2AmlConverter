using System;
using System.Numerics;
using System.Xml.Linq;
using AMLRider.Library.Extensions;
using AMLRider.Library.Helpers;
using AMLRider.Library.Rules.DataObjects;

namespace AMLRider.Library.Rules
{
    /// <inheritdoc />
    /// <summary>
    /// Responsible for the DeviceIdentityRule conversion.
    /// </summary>
    public class DeviceIdentityRule : IConversionRule
    {
        /// <summary>
        /// Checks if the rule can be applied to the given node.
        /// </summary>
        /// <param name="element">A given IODD element</param>
        /// <returns>True, if the rule can be applied. No, if not.</returns>
        public bool CanApplyRule(XElement element)
        {
            return element.Name == "DeviceIdentity";
        }

        /// <summary>
        /// Creates the AML element out of the given IODD element.
        /// </summary>
        /// <param name="element">A given IODD element.</param>
        /// <returns>The created AML element.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public XElement Apply(XElement element)
        {
            if(!CanApplyRule(element))
                throw new InvalidOperationException("The given node is not a <DeviceIdentity> node.");
            
            var deviceIdentityObj = GetDeviceIdentity(element);
            return CreateDeviceIdentityTag(deviceIdentityObj);
        }

        /// <summary>
        /// Gets the needed imformation out of the given IODD element.
        /// </summary>
        /// <param name="element">A given IODD element.</param>
        /// <returns>A DeviceIdentityObj which contains the necessary information.</returns>
        private static DeviceIdentityObj GetDeviceIdentity(XElement element)
        {
            var vendorText = element.Element("VendorText").GetAttributeValue("textId");
            var vendorUrl = element.Element("VendorUrl").GetAttributeValue("textId");
            var deviceName = element.Element("DeviceName").GetAttributeValue("textId");
            var deviceFamily = element.Element("DeviceFamily").GetAttributeValue("textId");
            var deviceIdentityObj = new DeviceIdentityObj
            {
                VendorId = element.GetAttributeValue("vendorId"),
                VendorName = element.GetAttributeValue("vendorName"),
                DeviceId = element.GetAttributeValue("deviceId"),
                VendorText = vendorText,
                VendorUrl = vendorUrl,
                DeviceName = deviceName,
                DeviceFamily = deviceFamily
            };

            return deviceIdentityObj;
        }

        /// <summary>
        /// Creates the AML DeviceIdentityTag.
        /// </summary>
        /// <param name="deviceIdentityObj">A DeviceIdentity object.</param>
        /// <returns>The created AML element.</returns>
        private static XElement CreateDeviceIdentityTag(DeviceIdentityObj deviceIdentityObj)
        {
            var internalElement = XmlHelper.CreateElement("InternalElement");
            internalElement.SetAttributeValue("Name", "DeviceIdentity");
            internalElement.SetAttributeValue("ID", "DeviceIdentity");
            internalElement.Add(CreateAttribute("vendorId", "integer", deviceIdentityObj.VendorId));
            internalElement.Add(CreateAttribute("vendorName", "string", deviceIdentityObj.VendorName));
            internalElement.Add(CreateAttribute("deviceId", "integer", deviceIdentityObj.DeviceId));
            internalElement.Add(CreateAttribute("VendorText", "string", deviceIdentityObj.VendorText));
            internalElement.Add(CreateAttribute("VendorUrl", "anyURI", deviceIdentityObj.VendorUrl));
            internalElement.Add(CreateAttribute("DeviceFamily", "string", deviceIdentityObj.DeviceFamily));
            var attribute = XmlHelper.CreateElement(internalElement, "Attribute");
            attribute.SetAttributeValue("Name", "DeviceName");
            attribute.SetAttributeValue("AttributeDataType", "xs:string");
            var value = XmlHelper.CreateElement(attribute, "Value", deviceIdentityObj.DeviceName);
            var refSemantic = XmlHelper.CreateElement(attribute, "RefSemantic");
            refSemantic.SetAttributeValue("CorrespondingAttributePath", "ECLASS:0173-1#02-AAO676#002");
            var attribute2 = XmlHelper.CreateElement(internalElement, "Attribute");
            attribute2.SetAttributeValue("Name", "ApplicationSpecificTag");
            attribute2.SetAttributeValue("AttributeDataType", "xs:string");
            var defaultValue = XmlHelper.CreateElement(attribute2, "DefaultValue", "");
            var attribute3 = XmlHelper.CreateElement(attribute2, "Attribute");
            attribute3.SetAttributeValue("Name", "accessRightRestriction");
            attribute3.SetAttributeValue("AttributeDataType", "xs:string");
            var value2 = XmlHelper.CreateElement(attribute3, "Value", "rw");
            internalElement.Add(CreateAttribute("V_HardwareRevision", "string", "1.0"));
            internalElement.Add(CreateAttribute("V_FirmwareRevision", "string", "1.0"));

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