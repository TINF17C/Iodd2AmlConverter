using System;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Helpers;
using Iodd2AmlConverter.Library.Rules.DataObjects;

namespace Iodd2AmlConverter.Library.Rules
{
    public class DatatypeRefRule : IConversionRule
    {
        /// <inheritdoc />
        public bool CanApplyRule(XElement element)
        {
            return element.Name.LocalName == "DatatypeRef";
        }
        
        /// <inheritdoc />
        public XElement Apply(XElement element)
        {
            if(!CanApplyRule(element))
                throw new InvalidOperationException("The given node is not a <DatatypeRef> node.");

            var documentInfo = GetDocumentInfo(element);
            return CreateInfoHeader(documentInfo);
        }
        
        /// <summary>
        /// Stores the attribute values of an <see cref="XElement"/> in a <see cref="DatatypeRefObj"/>.
        /// </summary>
        /// <param name="element">The element to parse.</param>
        /// <returns>The parsed <see cref="DocumentInfoObj"/>.</returns>
        private static DatatypeRefObj GetDocumentInfo(XElement element)
        {
            return new DatatypeRefObj
            {
                DatatypeId = element.GetAttributeValue("datatypeId")
            };
        }

        /// <summary>
        /// Creates the AdditionalInformationHeader element.
        /// </summary>
        /// <param name="obj">The <see cref="DatatypeRefRule"/> providing the necessary information.</param>
        /// <returns>The created <see cref="XElement"/>.</returns>
        private static XElement CreateInfoHeader(DatatypeRefObj obj)
        {
            var attributeHeader = XmlHelper.CreateElement("Attribute");
            attributeHeader.SetAttributeValue("Name", obj.DatatypeId);
            
            var refSemantic = XmlHelper.CreateElement(attributeHeader, "RefSemantic");
            refSemantic.SetAttributeValue("CorrespondingAttributePath", "ListType");

            var attributeINot = XmlHelper.CreateElement(attributeHeader, "Attribute");
            attributeINot.SetAttributeValue("Name", "Input not inverted");
            attributeINot.SetAttributeValue("AttributeDataType", "xs:string");
            
            var attributeI = XmlHelper.CreateElement(attributeHeader, "Attribute");
            attributeI.SetAttributeValue("Name", "Input inverted");
            attributeI.SetAttributeValue("AttributeDataType", "xs:string");
            
            return attributeHeader;
        }
        
    }
}