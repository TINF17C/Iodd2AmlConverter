using System;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Helpers;
using Iodd2AmlConverter.Library.Rules.DataObjects;

namespace Iodd2AmlConverter.Library.Rules
{
    public class CommNetworkProfileRule : IConversionRule
    {
        /// <inheritdoc />
        public bool CanApplyRule(XElement element)
        {
            return element.Name.LocalName == "CommNetworkProfile";
        }
        
        /// <inheritdoc />
        public XElement Apply(XElement element)
        {
            if(!CanApplyRule(element))
                throw new InvalidOperationException("The given node is not a <CommNetworkProfile> node.");

            //element.Descendants().FirstOrDefault(x => x.Name == "PhysicalLayer");
            
            var documentInfo = GetDocumentInfo(element);
            return CreateInfoHeader(documentInfo);
        }
        
        /// <summary>
        /// Stores the attribute values of an <see cref="XElement"/> in a <see cref="DatatypeRefObj"/>.
        /// </summary>
        /// <param name="element">The element to parse.</param>
        /// <returns>The parsed <see cref="DocumentInfoObj"/>.</returns>
        private static CommNetworkProfileObj GetDocumentInfo(XElement element)
        {
            var physicallayer = element.Descendants().FirstOrDefault(x => x.Name == "PhysicalLayer");
            var wire1 =element.Descendants().FirstOrDefault(x => x.Name == "Wire1");
            
            return new CommNetworkProfileObj
            {
                MinCycleTime = physicallayer.GetAttributeValue("minCycleTime"),
                SioSupported = physicallayer.GetAttributeValue("sioSupported"),
                Bitrate = physicallayer.GetAttributeValue("bitrate"),
                MSequenceCapability = physicallayer.GetAttributeValue("mSequenceCapability"),
                Function = wire1.GetAttributeValue("function"),
                Color = wire1.GetAttributeValue("color"),
              
            };
        }

        /// <summary>
        /// Creates the AdditionalInformationHeader element.
        /// </summary>
        /// <param name="obj">The <see cref="DatatypeRefRule"/> providing the necessary information.</param>
        /// <returns>The created <see cref="XElement"/>.</returns>
        private static XElement CreateInfoHeader(CommNetworkProfileObj obj)
        {
            /// <InternalElement Name="IO-Link Device Port" ID="GUID0">
           
            var InternalElement = XmlHelper.CreateElement("InternalElement");
            InternalElement.SetAttributeValue("Name", "IO-Link Device Port");
            InternalElement.SetAttributeValue("ID", "GUID0");

            // <Attribute Name="minCycleTime" AttributeDataType="xs:integer">
            // <Value>3500</Value>
            var attribute1 = XmlHelper.CreateElement(InternalElement, "Attribute");
            attribute1.SetAttributeValue("Name", obj.MinCycleTime);
            attribute1.SetAttributeValue("AttributeDataType", "xs:integer");
            XmlHelper.CreateElement(attribute1, "Value", obj.MinCycleTime); //foreach check

            var attribute2 = XmlHelper.CreateElement(InternalElement, "Attribute");
            attribute2.SetAttributeValue("Name", obj.SioSupported);
            attribute2.SetAttributeValue("AttributeDataType", "xs:boolean");
            XmlHelper.CreateElement(attribute2, "Value", obj.SioSupported); //foreach check

            var attribute3 = XmlHelper.CreateElement(InternalElement, "Attribute");
            attribute3.SetAttributeValue("Name", obj.Bitrate);
            attribute3.SetAttributeValue("AttributeDataType", "xs:string");
            XmlHelper.CreateElement(attribute3, "Value", obj.Bitrate); //foreach check

            var attribute4 = XmlHelper.CreateElement(InternalElement, "Attribute");
            attribute4.SetAttributeValue("Name", obj.MSequenceCapability);
            attribute4.SetAttributeValue("AttributeDataType", "xs:integer");
            XmlHelper.CreateElement(attribute4, "Value", obj.MSequenceCapability); //foreach check

            
            // <ExternalInterface Name="IO-Link Device Port" ID="GUID1"
            // RefBaseClassPath="physicalEndPoint/IOLD">
            // <Attribute Name="Socket" AttributeDataType="xsi:type">
            // <Value>M12-4ConnectionT</Value>

            var externalInterface = XmlHelper.CreateElement(InternalElement, "ExternalInterface");
            externalInterface.SetAttributeValue("Name", "IO-Link Device Port"); 
            externalInterface.SetAttributeValue("ID","GUID1"); 
            externalInterface.SetAttributeValue("RefBaseClassPath","physicalEndPoint/IOLD"); 
           
            var externalInterfaceAttribute = XmlHelper.CreateElement(externalInterface, "Attribute");
            externalInterfaceAttribute.SetAttributeValue("Name", "Socket"); 
            externalInterfaceAttribute.SetAttributeValue("AttributeDataType", "xsi:type"); 
            
            XmlHelper.CreateElement(externalInterfaceAttribute, "Value","M12-4ConnectionT");

            // <InternalElement Name="Wire1" ID="GUID11">
            var InternalElement2 = XmlHelper.CreateElement(InternalElement, "InternalElement");
            InternalElement2.SetAttributeValue("Name", "Wire1"); 
            InternalElement2.SetAttributeValue("ID","GUID11"); 
            
            var externalInterfaceInternal = XmlHelper.CreateElement(InternalElement2, "ExternalInterface");
            externalInterfaceInternal.SetAttributeValue("Name", "Pin1"); 
            externalInterfaceInternal.SetAttributeValue("ID", "GUID11"); 
            
            var attFunction = XmlHelper.CreateElement(externalInterfaceInternal, "ExternalInterface");
            attFunction.SetAttributeValue("Name", "function");
            attFunction.SetAttributeValue("AttributeDataType", "xs:string");
            XmlHelper.CreateElement(attFunction, "Value", obj.Function);

            var attColor = XmlHelper.CreateElement(externalInterfaceInternal, "ExternalInterface");
            attColor.SetAttributeValue("Name", "color");
            attColor.SetAttributeValue("AttributeDataType", "xs:string");
            XmlHelper.CreateElement(attColor, "Value", obj.Color);

            
            return InternalElement;
        }
    }
}