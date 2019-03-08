using System;
using System.Xml;
using System.Xml.Linq;
using AMLRider.Library.Helpers;

namespace AMLRider.Library.Rules
{
    
    public class DocumentInfoRule : IConversionRule
    {

        private XElement CreateWriterHeader()
        {
            var writerHeader = new XElement("WriterHeader");

            var writerName = XmlHelper.CreateElementWithValue("WriterName", "AMLRider");
            writerHeader.Add(writerName);

            var guid = Guid.NewGuid().ToString();
            var writerId = XmlHelper.CreateElementWithValue("WriterID", guid);
            writerHeader.Add(writerId);

            return writerHeader;
        }

        public bool CanApplyRule(XElement element)
        {
            return element.Name == "DocumentInfo";
        }

        public XElement Apply(XElement element)
        {
            if(!CanApplyRule(element))
                throw new InvalidOperationException("The given node is not a <DocumentInfo> node.");

            return CreateWriterHeader();
        }
        
    }
    
}