using System;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{
    
    public class DocumentInfo : IoddElement
    {
        
        /// <summary>
        /// Version of the concrete instance, not of the IODD specification.
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// The release date of the IO-Link device.
        /// </summary>
        public string ReleaseDate { get; set; }
        
        /// <summary>
        /// The vendor specific copyright text.
        /// </summary>
        public string Copyright { get; set; }

        public override void Deserialize(XElement element)
        {
            Version = element.GetAttributeValue("version");
            ReleaseDate = element.GetAttributeValue("releaseDate");
            Copyright = element.GetAttributeValue("copyright");
        }

        public override AmlElement ToAml()
        {
            return new AdditionalInformation()
            {
                WriterVendor = Copyright
            };
        }
    }
    
}