using System;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test
{
    
    [TestClass]
    public class DeviceIdentityRuleTest
    {

        private const string VendorId = "888";
        private const string VendorName = "BALLUFF";
        private const string DeviceId = "262658";
        
        private XElement XmlSampleElement { get; set; }
        
        private XElement XmlSampleElementWrong { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            var sample =
                $"<DeviceIdentity vendorId=\"{VendorId}\" vendorName=\"{VendorName}\" deviceId=\"{DeviceId}\">\n\t<VendorText textId=\"TI_VendorText\" />\n\t<VendorUrl textId=\"TI_VendorUrl\" />\n\t<VendorLogo name=\"Balluff-logo.png\" />\n\t<DeviceName textId=\"TI_DeviceName\" />\n\t<DeviceFamily textId=\"TI_DeviceFamily\" />\n</DeviceIdentity>\n";
            const string wrongSample =
                "<SomeIdentity vendorId=\"888\" vendorName=\"BALLUFF\" deviceId=\"262658\">\n\t<VendorText textId=\"TI_VendorText\" />\n\t<VendorUrl textId=\"TI_VendorUrl\" />\n\t<VendorLogo name=\"Balluff-logo.png\" />\n\t<DeviceName textId=\"TI_DeviceName\" />\n\t<DeviceFamily textId=\"TI_DeviceFamily\" />\n</SomeIdentity>\n";
            
            
            XmlSampleElement = XElement.Parse(sample);
            XmlSampleElementWrong = XElement.Parse(wrongSample);
        }
        
        [TestMethod]
        public void CanApplyRuleReturnsTrue()
        {
            var rule = new DeviceIdentityRule();

            var result = rule.CanApplyRule(XmlSampleElement);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanApplyRuleReturnsFalse()
        {
            var rule = new DeviceIdentityRule();

            var result = rule.CanApplyRule(XmlSampleElementWrong);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanApplyRuleThrowsException()
        {
            var rule = new DeviceIdentityRule();
            rule.Apply(XmlSampleElementWrong);
        }

        [TestMethod]
        public void IsRootElementInternalElement()
        {
            var rule = new DeviceIdentityRule();

            var element = rule.Apply(XmlSampleElement);
            Assert.AreEqual("InternalElement", element.Name);
        }

        [TestMethod]
        public void IsVendorIdSetCorrectly()
        {
            var rule = new DeviceIdentityRule();

            var element = rule.Apply(XmlSampleElement);
            var value = FindValueByAttributeName(element, "vendorId");
            
            Assert.AreEqual(VendorId, value);
        }
        
        [TestMethod]
        public void IsVendorNameSetCorrectly()
        {
            var rule = new DeviceIdentityRule();

            var element = rule.Apply(XmlSampleElement);
            var value = FindValueByAttributeName(element, "vendorName");
            
            Assert.AreEqual(VendorName, value);
        }
        
        [TestMethod]
        public void IsDeviceIdSetCorrectly()
        {
            var rule = new DeviceIdentityRule();

            var element = rule.Apply(XmlSampleElement);
            var value = FindValueByAttributeName(element, "deviceId");
            
            Assert.AreEqual(DeviceId, value);
        }

        private static string FindValueByAttributeName(XContainer element, string attributeName)
        {
            return element
                .Elements("Attribute")
                .FirstOrDefault(x => x.Attribute("Name")?.Value == attributeName)?
                .Element("Value")?
                .Value;
        }
        
    }
    
}