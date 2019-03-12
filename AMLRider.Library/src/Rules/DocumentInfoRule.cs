using System;
using System.Xml.Linq;
using AMLRider.Library.Extensions;
using AMLRider.Library.Helpers;
using AMLRider.Library.Rules.DataObjects;

namespace AMLRider.Library.Rules
{
    
    /// <inheritdoc />
    /// <summary>
    /// Responsible for parsing the DocumentInfo tag.
    /// </summary>
    public class DocumentInfoRule : IConversionRule
    {

        /// <summary>
        /// Stores the attribute values of an <see cref="XElement"/> in a <see cref="DocumentInfoObj"/>.
        /// </summary>
        /// <param name="element">The element to parse.</param>
        /// <returns>The parsed <see cref="DocumentInfoObj"/>.</returns>
        private static DocumentInfoObj GetDocumentInfo(XElement element)
        {
            return new DocumentInfoObj
            {
                Copyright = element.GetAttributeValue("copyright"),
                ReleaseDate = element.GetAttributeValue("releaseDate"),
                Version = element.GetAttributeValue("version")
            };
        }
        
        /// <summary>
        /// Creates the AdditionalInformationHeader element.
        /// </summary>
        /// <param name="obj">The <see cref="DocumentInfoObj"/> providing the necessary information.</param>
        /// <returns>The created <see cref="XElement"/>.</returns>
        private static XElement CreateInfoHeader(DocumentInfoObj obj)
        {
            var infoHeader = XmlHelper.CreateElement("AdditionalInformation");
            var writerHeader = XmlHelper.CreateElement(infoHeader, "WriterHeader");

            XmlHelper.CreateElement(writerHeader, "WriterName", "AMLRider");
            XmlHelper.CreateElement(writerHeader, "WriterID", Guid.NewGuid().ToString());
            XmlHelper.CreateElement(writerHeader, "WriterVendor", obj.Copyright);
            XmlHelper.CreateElement(writerHeader, "WriterVendorURL");
            XmlHelper.CreateElement(writerHeader, "WriterVersion", obj.Version);
            XmlHelper.CreateElement(writerHeader, "WriterRelease", obj.ReleaseDate);
            XmlHelper.CreateElement(writerHeader, "LastWritingDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            return infoHeader;
        }

        /// <inheritdoc />
        public bool CanApplyRule(XElement element)
        {
            return element.Name == "DocumentInfo";
        }

        /// <inheritdoc />
        public XElement Apply(XElement element)
        {
            if(!CanApplyRule(element))
                throw new InvalidOperationException("The given node is not a <DocumentInfo> node.");

            var documentInfo = GetDocumentInfo(element);
            return CreateInfoHeader(documentInfo);
        }
        
    }
    
}