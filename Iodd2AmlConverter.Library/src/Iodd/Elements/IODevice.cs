using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;
using Iodd2AmlConverter.Library.Extensions;

namespace Iodd2AmlConverter.Library.Iodd.Elements
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

        public override AmlCollection ToAml()
        {
            var systemUnitClass = new SystemUnitClass
            {
                Name = "Device"
            };
            
            systemUnitClass.InternalElements.AddRange(ProfileHeader.ToAml().Cast<InternalElement>());
            systemUnitClass.InternalElements.AddRange(ProfileBody.ToAml().Cast<InternalElement>());
            systemUnitClass.InternalElements.AddRange(ExternalTextCollection.ToAml().Cast<InternalElement>());
            
            var file = new CaexFile
            {
                AdditionalInformation = DocumentInfo.ToAml().First() as AdditionalInformation,
                SystemUnitClassLib = new SystemUnitClassLib
                {
                    Name = "ComponentSystemUnitClassLib",
                    SystemUnitClass = systemUnitClass
                }
            };

            return AmlCollection.Of(file);
        }
    }
}