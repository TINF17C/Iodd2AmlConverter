using System.Linq;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{
    public class IODevice : IoddElement
    {
        
        public DocumentInfo DocumentInfo { get; set; }
        
        public ProfileHeader ProfileHeader { get; set; }
        
        public ProfileBody ProfileBody { get; set; }
        
        public ExternalTextCollection ExternalTextCollection { get; set; }
        
        public override void Deserialize(XElement element)
        {         
            DocumentInfo = new DocumentInfo();
            DocumentInfo.Deserialize(element.SubElement("DocumentInfo"));
            
            ProfileHeader = new ProfileHeader();
            ProfileHeader.Deserialize(element.SubElement("ProfileHeader"));
            
            ProfileBody = new ProfileBody();
            ProfileBody.Deserialize(element.SubElement("ProfileBody"));
            
            ExternalTextCollection = new ExternalTextCollection();
            ExternalTextCollection.Deserialize(element.SubElement("ExternalTextCollection"));
        }

        public override AmlElement ToAml()
        {
            var file = new CaexFile
            {
                AdditionalInformation = DocumentInfo.ToAml() as AdditionalInformation
            };

            file.InternalElements.Add(ProfileHeader.ToAml() as InternalElement);
            file.InternalElements.Add(ProfileBody.ToAml() as InternalElement);

            return file;
        }
    }
}