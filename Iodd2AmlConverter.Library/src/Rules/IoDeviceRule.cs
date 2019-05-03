using System;
using System.Xml.Linq;
using System.Xml.Schema;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Helpers;
using Iodd2AmlConverter.Library.Rules.DataObjects;

namespace Iodd2AmlConverter.Library.Rules
{
    /// <inheritdoc />
    /// <summary>
    /// Responsible for the conversion of the IODevice Element.
    /// </summary>
    public class IoDeviceRule : IConversionRule
    {
        /// <inheritdoc />
        /// <summary>
        /// Checks if the rule can be applied to the given element.
        /// </summary>
        /// <param name="element">The given IODD element</param>
        /// <returns>True, if the rule can be applied. False if not.</returns>
        public bool CanApplyRule(XElement element)
        {
            return element.Name.LocalName == "IODevice";
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates the CaexFileTag out of the IODevice element.
        /// </summary>
        /// <param name="element">The given IODD element.</param>
        /// <returns>The created AML element.</returns>
        /// <exception cref="T:System.InvalidOperationException"></exception>
        public XElement Apply(XElement element)
        {
            if (!CanApplyRule(element))
                throw new InvalidOperationException("The given node is not a <IODevice> node.");

            var ioDeviceDataObj = GetIoDeviceData(element);
            return CreateCaexFileTag(ioDeviceDataObj);
        }

        /// <summary>
        /// Gets the SchemaInstance from the IODevice element.
        /// </summary>
        /// <param name="element">The given IODD element.</param>
        /// <returns>A IoDeviceDataObj</returns>
        private static IoDeviceDataObj GetIoDeviceData(XElement element)
        {
            var ioDeviceDataObj = new IoDeviceDataObj
            {
                SchemaInstance = element.GetNamespaceOfPrefix("xsi")?.NamespaceName,
                NameSpace = element.GetNamespaceOfPrefix("xsi")
            };

            return ioDeviceDataObj;
        }

        /// <summary>
        /// Creates the CaexFile element.
        /// </summary>
        /// <param name="ioDeviceDataObj">An IoDeviceDataObj</param>
        /// <returns>The created CaexFile element.</returns>
        private static XElement CreateCaexFileTag(IoDeviceDataObj ioDeviceDataObj)
        {
            var xsi = ioDeviceDataObj.SchemaInstance;
            var xsiNameSpace = ioDeviceDataObj.NameSpace;
            const string noNamespaceSchemaLocation = "CAEX_ClassModel_V2.15.xsd";
            var caexFileElement = new XElement("CAEXFile", new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(xsiNameSpace + "noNamespaceSchemaLocation", noNamespaceSchemaLocation));
            caexFileElement.SetAttributeValue("SchemaVersion", "2.15");
            caexFileElement.SetAttributeValue("FileName", "IODDfilename.aml");
            var additionalInformation = XmlHelper.CreateElement(caexFileElement, "AdditionalInformation");
            additionalInformation.SetAttributeValue("AutomationMLVersion", "2.0");

            return caexFileElement;
        }
    }
}