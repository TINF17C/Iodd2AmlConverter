using System;
using System.Linq;
using System.Xml.Linq;
using AMLRider.Library.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMLRider.Library.Test
{
    [TestClass]
    public class FeaturesRuleTest
    {
        private const string BlockParameterText = "true";
        private const string DataStorageText = "true";

        private const string ParameterText = "false";
        private const string InnerDataStorageText = "true";
        private const string LocalUserInterfaceText = "false";
        private const string LocalParameterizationText = "false";

        private XElement XmlSampleElement { get; set; }

        private XElement XmlSampleElementWrong { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            var sample =
                $"<Features blockParameter=\"{BlockParameterText}\" dataStorage=\"{DataStorageText}\">\n\t<SupportedAccessLocks parameter=\"{ParameterText}\" dataStorage=\"{InnerDataStorageText}\" localUserInterface=\"{LocalUserInterfaceText}\" localParameterization=\"{LocalParameterizationText}\" />\n</Features>\n";
            const string wrongSample =
                "<OtherFeatures blockParameter=\"true\" dataStorage=\"true\">\n\t<SupportedAccessLocks parameter=\"false\" dataStorage=\"true\" localUserInterface=\"false\" localParameterization=\"false\" />\n</OtherFeatures>\n";


            XmlSampleElement = XElement.Parse(sample);
            XmlSampleElementWrong = XElement.Parse(wrongSample);
        }

        [TestMethod]
        public void CanApplyRuleReturnsTrue()
        {
            var rule = new FeaturesRule();

            var result = rule.CanApplyRule(XmlSampleElement);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanApplyRuleReturnsFalse()
        {
            var rule = new FeaturesRule();

            var result = rule.CanApplyRule(XmlSampleElementWrong);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanApplyRuleThrowsException()
        {
            var rule = new FeaturesRule();
            rule.Apply(XmlSampleElementWrong);
        }

        [TestMethod]
        public void IsRootElementInternalElement()
        {
            var rule = new FeaturesRule();

            var element = rule.Apply(XmlSampleElement);
            Assert.AreEqual("InternalElement", element.Name);
        }

        [TestMethod]
        public void IsBlockParameterSetCorrectly()
        {
            var rule = new FeaturesRule();

            var element = rule.Apply(XmlSampleElement);
            var value = GetAttributeValue(element, "blockParameter");

            Assert.AreEqual(BlockParameterText, value);
        }

        [TestMethod]
        public void IsDataStorageSetCorrectly()
        {
            var rule = new FeaturesRule();

            var element = rule.Apply(XmlSampleElement);
            var value = GetAttributeValue(element, "dataStorage");

            Assert.AreEqual(DataStorageText, value);
        }

        [TestMethod]
        public void HasSubInternalElementSupportedAccessLocks()
        {
            var rule = new FeaturesRule();

            var element = rule.Apply(XmlSampleElement);
            var result = element
                .Elements("InternalElement")
                .FirstOrDefault(x => x.Attribute("Name")?.Value == "SupportedAccessLocks");
            
            Assert.IsNotNull(result);
        }
        
        [TestMethod]
        public void IsParameterSetCorrectly()
        {
            var rule = new FeaturesRule();

            var element = rule.Apply(XmlSampleElement);
            var value = GetSubAttributeValue(element, "parameter");

            Assert.AreEqual(ParameterText, value);
        }
        
        [TestMethod]
        public void IsInnerDataStorageSetCorrectly()
        {
            var rule = new FeaturesRule();

            var element = rule.Apply(XmlSampleElement);
            var value = GetSubAttributeValue(element, "dataStorage");

            Assert.AreEqual(InnerDataStorageText, value);
        }
        
        [TestMethod]
        public void IsLocalUserInterfaceSetCorrectly()
        {
            var rule = new FeaturesRule();

            var element = rule.Apply(XmlSampleElement);
            var value = GetSubAttributeValue(element, "localUserInterface");

            Assert.AreEqual(LocalUserInterfaceText, value);
        }
        
        [TestMethod]
        public void IsLocalParameterizationSetCorrectly()
        {
            var rule = new FeaturesRule();

            var element = rule.Apply(XmlSampleElement);
            var value = GetSubAttributeValue(element, "localParameterization");

            Assert.AreEqual(LocalParameterizationText, value);
        }

        private static string GetAttributeValue(XContainer element, string attributeName)
        {
            return element
                .Elements("Attribute")
                .FirstOrDefault(x => x.Attribute("Name")?.Value == attributeName)?
                .Element("DefaultValue")?
                .Value;
        }

        private static string GetSubAttributeValue(XContainer element, string attributeName)
        {
            return element
                .Elements("InternalElement")
                .FirstOrDefault(x => x.Attribute("Name")?.Value == "SupportedAccessLocks")?
                .Elements("Attribute")
                .FirstOrDefault(x => x.Attribute("Name")?.Value == attributeName)?
                .Value;
        }
    }
}