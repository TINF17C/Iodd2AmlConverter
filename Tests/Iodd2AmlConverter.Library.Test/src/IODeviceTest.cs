using System.Xml.Linq;
using Iodd2AmlConverter.Library.Iodd.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test
{

    [TestClass]
    public class IODeviceTest
    {

        private const string XmlText =
            @"<IODevice>
	            <DocumentInfo />
	            <ProfileHeader />
	            <ProfileBody />
	            <ExternalTextCollection />
            </IODevice>";
        
        private IODevice Device { get; set; }
        
        [TestInitialize]
        public void Initialize()
        {
            Device = new IODevice();
            Device.Deserialize(XElement.Parse(XmlText));
        }
        
        [TestMethod]
        public void HasDocumentInfo()
        {
            Assert.IsNotNull(Device.DocumentInfo);
        }
        
        [TestMethod]
        public void HasProfileHeader()
        {
            Assert.IsNotNull(Device.ProfileHeader);
        }
        
        [TestMethod]
        public void HasProfileBody()
        {
            Assert.IsNotNull(Device.ProfileBody);
        }

        [TestMethod]
        public void HasExternalTextCollection()
        {
            Assert.IsNotNull(Device.ExternalTextCollection);
        }

        [TestMethod]
        public void IsCorrectAml()
        {
            var amlCollection = Device.ToAml();
            Assert.IsNotNull(amlCollection);
        }

    }

}