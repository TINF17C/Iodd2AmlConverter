using System.Collections.Generic;
using System.Xml.Linq;

namespace Iodd2AmlConverter.Library.Aml.Elements
{

    public class CaexFile : AmlElement
    {

        private const string XsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";
        private const string NoNamespaceSchemaLocation = "CAEX_ClassModel_V2.15.xsd";
        private const string CaexVersion = "2.15";

        public string SchemaVersion { get; set; }

        public string FileName { get; set; }

        public AdditionalInformation AdditionalInformation { get; set; }

        public SystemUnitClassLib SystemUnitClassLib { get; set; }

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

            element.SetAttributeValue("SchemaVersion", CaexVersion);
            element.SetAttributeValue("FileName", FileName ?? string.Empty);

            if (AdditionalInformation != null)
            {
                var infoElement = AdditionalInformation.Serialize();
                element.Add(infoElement);
            }

            if (SystemUnitClassLib != null)
            {
                var classElement = SystemUnitClassLib.Serialize();
                element.Add(classElement);
            }

            foreach (var internalElement in InternalElements)
            {
                var xmlElement = internalElement.Serialize();
                element.Add(xmlElement);
            }

            return element;
        }

    }

}