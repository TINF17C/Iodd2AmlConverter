using System.Xml.Linq;
using AMLRider.Library.Aml;

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
            DocumentInfo.Deserialize(element.Element("DocumentInfo"));
            
            ProfileHeader = new ProfileHeader();
            ProfileHeader.Deserialize(element.Element("ProfileHeader"));
            
            ProfileBody = new ProfileBody();
            ProfileBody.Deserialize(element.Element("ProfileBody"));
            
            ExternalTextCollection = new ExternalTextCollection();
            ExternalTextCollection.Deserialize(element.Element("ExternalTextCollection"));
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
    }
}