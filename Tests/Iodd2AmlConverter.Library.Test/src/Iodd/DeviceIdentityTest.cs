using System.Xml.Linq;
using Iodd2AmlConverter.Library.Iodd.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test.Iodd
{

    [TestClass]
    public class DeviceIdentityTest
    {

        private const string XmlText =
            @"<DeviceIdentity vendorId=""310"" vendorName=""ifm electronic gmbh"" deviceId=""159"">
	            <VendorText textId=""TI_VendorText"" />
	            <VendorUrl textId=""TI_VendorUrl"" />
	            <VendorLogo name=""ifm-logo.png"" />
	            <DeviceFamily textId=""TI_DeviceFamily"" />
            </DeviceIdentity>";

        private DeviceIdentity Identity { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Identity = new DeviceIdentity();
            Identity.Deserialize(XElement.Parse(XmlText));
        }
        
        [TestMethod]
        public void HasCorrectVendorId()
        {
            Assert.AreEqual(310, Identity.VendorId);
        }
        
        [TestMethod]
        public void HasCorrectVendorName()
        {
            Assert.AreEqual("ifm electronic gmbh", Identity.VendorName);
        }
        
        [TestMethod]
        public void HasCorrectDeviceId()
        {
            Assert.AreEqual("159", Identity.DeviceId);
        }

        [TestMethod]
        public void HasCorrectVendorText()
        {
            Assert.AreEqual("TI_VendorText", Identity.VendorText);
        }

        [TestMethod]
        public void HasCorrectVendorUrl()
        {
            Assert.AreEqual("TI_VendorUrl", Identity.VendorUrl);
        }

        [TestMethod]
        public void HasCorrectVendorLogo()
        {
            Assert.AreEqual("ifm-logo.png", Identity.VendorLogo);
        }

        [TestMethod]
        public void HasCorrectDeviceFamily()
        {
            Assert.AreEqual("TI_DeviceFamily", Identity.DeviceFamily);
        }

        [TestMethod]
        public void IsCorrectAml()
        {
            var amlCollection = Identity.ToAml();
            Assert.IsNotNull(amlCollection);
        }

    }

}