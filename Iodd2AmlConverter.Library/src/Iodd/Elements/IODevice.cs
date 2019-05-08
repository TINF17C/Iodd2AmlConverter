using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;

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

        public override AmlElement ToAml()
        {
            var systemUnitClass = new SystemUnitClass();
            systemUnitClass.InternalElements.Add(ProfileHeader.ToAml() as InternalElement);
            systemUnitClass.InternalElements.Add(ProfileBody.ToAml() as InternalElement);
            
            var file = new CaexFile
            {
                AdditionalInformation = DocumentInfo.ToAml() as AdditionalInformation,
                SystemUnitClassLib = new SystemUnitClassLib
                {
                    SystemUnitClass = systemUnitClass
                }
            };

            file.InternalElements.Add(ExternalTextCollection.ToAml() as InternalElement);
            
            return file;
        }
    }
}