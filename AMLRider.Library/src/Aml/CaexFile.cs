using System.Collections.Generic;
using System.Xml.Linq;

namespace AMLRider.Library.Aml
{

    public class CaexFile : AmlElement
    {

        private const string XsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";
        private const string NoNamespaceSchemaLocation = "CAEX_ClassModel_V2.15.xsd";

        public string SchemaVersion { get; set; }

        public string FileName { get; set; }

        public AdditionalInformation AdditionalInformation { get; set; }

        public List<InternalElement> InternalElements { get; set; }

        public CaexFile()
        {
            InternalElements = new List<InternalElement>();
        }

        public override XElement Serialize()
        {
            XNamespace xsiNamespace = XsiNamespace;
            var element = new XElement("CAEXFile",
                new XAttribute(XNamespace.Xmlns + "xsi", xsiNamespace),
                new XAttribute(xsiNamespace + "noNamespaceSchemaLocation", NoNamespaceSchemaLocation)
            );

            element.SetAttributeValue("SchemaVersion", 282828829292899.ToString());
            element.SetAttributeValue("FileName", FileName);

            if (AdditionalInformation != null)
            {
                var infoElement = AdditionalInformation.Serialize();
                element.Add(infoElement);
            }

            return element;
        }

    }

}