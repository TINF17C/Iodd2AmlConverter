using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using AMLRider.Library.Helpers;

namespace AMLRider.Library.Aml
{
    public class AdditionalInformation : AmlElement
    {

        [DefaultValue("AMLRider")]
        public string WriterName { get; set; } = "AMLRider";

        public string WriterId { get; set; } = Guid.NewGuid().ToString();
        
        public string WriterVendor { get; set; }
        
        public string WriterVendorUrl { get; set; }

        [DefaultValue("1.0.0")]
        public string WriterVersion { get; set; } = "1.0.0";

        [DefaultValue("1.0.0")]
        public string WriterRelease { get; set; } = "1.0.0";
        
        public DateTime LastWritingDateTime { get; set; }

        [DefaultValue("IODD1.1")]
        public string WriterProjectTitle { get; set; } = "IODD1.1";

        [DefaultValue("unspecified")]
        public string WriterProjectId { get; set; } = "unspecified";
        
        public override XElement Serialize()
        {
            var root = XmlHelper.CreateElement("AdditionalInformation");
            var writerHeader = XmlHelper.CreateElement(root, "WriterHeader");

            XmlHelper.CreateElement(writerHeader, "WriterName", WriterName ?? string.Empty);
            XmlHelper.CreateElement(writerHeader, "WriterID", WriterId ?? string.Empty);
            XmlHelper.CreateElement(writerHeader, "WriterVendor", WriterVendor ?? string.Empty);
            XmlHelper.CreateElement(writerHeader, "WriterVendorURL", WriterVendorUrl ?? string.Empty);
            XmlHelper.CreateElement(writerHeader, "WriterVersion", WriterVersion ?? string.Empty);
            XmlHelper.CreateElement(writerHeader, "WriterRelease", WriterRelease ?? string.Empty);
            XmlHelper.CreateElement(writerHeader, "LastWritingDateTime", LastWritingDateTime.ToString(CultureInfo.InvariantCulture));
            XmlHelper.CreateElement(writerHeader, "WriterProjectTitle", WriterProjectTitle ?? string.Empty);
            XmlHelper.CreateElement(writerHeader, "WriterProjectID", WriterProjectId ?? string.Empty);

            return root;
        }
    }
}