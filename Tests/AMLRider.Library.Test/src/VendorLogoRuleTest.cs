using System;
using System.Linq;
using System.Xml.Linq;
using AMLRider.Library.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMLRider.Library.Test
{
    
    [TestClass]
    public class VendorLogoRuleTest
    {

        private const string VendorLogoName = "Balluff-logo.png";

        private XElement XmlSampleElement { get; set; }
        
        private XElement XmlSampleElementWrong { get; set; }
        
        [TestInitialize]
        public void Initialize()
        {
            var sample = $"<VendorLogo name=\"{VendorLogoName}\" />";
            var wrongSample = $"<SomeLogo name=\"{VendorLogoName}\" />";
            
            
            XmlSampleElement = XElement.Parse(sample);
            XmlSampleElementWrong = XElement.Parse(wrongSample);
        }
        
        [TestMethod]
        public void CanApplyRuleReturnsTrue()
        {
            var rule = new VendorLogoRule();

            var result = rule.CanApplyRule(XmlSampleElement);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanApplyRuleReturnsFalse()
        {
            var rule = new VendorLogoRule();

            var result = rule.CanApplyRule(XmlSampleElementWrong);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanApplyRuleThrowsException()
        {
            var rule = new VendorLogoRule();
            rule.Apply(XmlSampleElementWrong);
        }
        
        [TestMethod]
        public void IsRootElementInternalElement()
        {
            var rule = new VendorLogoRule();

            var element = rule.Apply(XmlSampleElement);
            Assert.AreEqual("InternalElement", element.Name);
        }

        [TestMethod]
        public void HasResultDescendents()
        {
            var rule = new VendorLogoRule();

            var element = rule.Apply(XmlSampleElement);
            var descendents = element.Descendants();
            
            Assert.IsTrue(descendents.Any());
        }

        [TestMethod]
        public void HasSingleValueElement()
        {
            var rule = new VendorLogoRule();
            var element = rule.Apply(XmlSampleElement);

            var valueElement = element
                .Descendants()
                .FirstOrDefault(x => x.Name == "Value");
            
            Assert.IsNotNull(valueElement);
        }

        [TestMethod]
        public void IsVendorLogoNameSetCorrectly()
        {
            var rule = new VendorLogoRule();
            var element = rule.Apply(XmlSampleElement);

            var valueElement = element
                .Descendants()
                .FirstOrDefault(x => x.Name == "Value");
            
            Assert.AreEqual(VendorLogoName, valueElement?.Value);
        }
        
        
    }
    
}