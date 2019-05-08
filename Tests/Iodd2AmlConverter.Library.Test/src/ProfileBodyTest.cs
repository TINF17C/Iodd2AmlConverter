using System.Xml.Linq;
using Iodd2AmlConverter.Library.Iodd.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test
{

    [TestClass]
    public class ProfileBodyTest
    {

        private const string XmlText =
            @"<ProfileBody>
	            <DeviceIdentity />
	            <DeviceFunction />
            </ProfileBody>";
        
        private ProfileBody Body { get; set; }
        
        [TestInitialize]
        public void Initialize()
        {
            Body = new ProfileBody();
            Body.Deserialize(XElement.Parse(XmlText));
        }
        
        [TestMethod]
        public void HasDeviceIdentity()
        {
            Assert.IsNotNull(Body.DeviceIdentity);
        }
        
        [TestMethod]
        public void HasDeviceFunction()
        {
            Assert.IsNotNull(Body.DeviceFunction);
        }

        [TestMethod]
        public void IsCorrectAml()
        {
            var amlCollection = Body.ToAml();
            Assert.IsNotNull(amlCollection);
        }

    }

}