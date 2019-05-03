using System;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test
{
    [TestClass]
    public class IoDeviceRuleTest
    {
        private const string SchemaInstance = "http://www.w3.org/2001/XMLSchema-instance";
        
        private XElement XmlSampleElement { get; set; }
        
        private XElement XmlSampleElementWrong { get; set; }
        
        [TestInitialize]
        public void Initialize()
        {
            var sample = $"<IODevice xmlns=\"http://www.io-link.com/IODD/2010/10\" xmlns:xsi=\"{SchemaInstance}\" xsi:schemaLocation=\"http://www.io-link.com/IODD/2010/10 IODD1.1.xsd\"></IODevice>";
            const string wrongSample =
                @"<SomeDevice xmlns=""http://www.io-link.com/IODD/2010/10"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.io-link.com/IODD/2010/10 IODD1.1.xsd"" ></SomeDevice>";
                        
            XmlSampleElement = XElement.Parse(sample);
            XmlSampleElementWrong = XElement.Parse(wrongSample);
        }
        
        [TestMethod]
        public void CanApplyRuleReturnsTrue()
        {
            var rule = new IoDeviceRule();

            var result = rule.CanApplyRule(XmlSampleElement);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanApplyRuleReturnsFalse()
        {
            var rule = new IoDeviceRule();
            
            var result = rule.CanApplyRule(XmlSampleElementWrong);
            Assert.IsFalse(result);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanApplyRuleThrowsException()
        {
            var rule = new IoDeviceRule();
            rule.Apply(XmlSampleElementWrong);
        }

        [TestMethod]
        public void IsRootElementCaexFile()
        {
            var rule = new IoDeviceRule();

            var element = rule.Apply(XmlSampleElement);
            Assert.AreEqual("CAEXFile", element.Name);
        }
        
        [TestMethod]
        public void HasCorrectSubElements()
        {
            var rule = new IoDeviceRule();

            var element = rule.Apply(XmlSampleElement);
            var subElementCount = element.Elements().Count();
            var subElement = element.Elements().First();
            
            Assert.AreEqual(1, subElementCount);
            Assert.AreEqual(subElement.Name, "AdditionalInformation");
        }
        
        [TestMethod]
        public void IsSchemaInstanceSetCorrectly()
        {
            var rule = new IoDeviceRule();

            var schemaInstance = rule.Apply(XmlSampleElement).GetNamespaceOfPrefix("xsi")?.NamespaceName;
            
            Assert.AreEqual(SchemaInstance, schemaInstance);
        }
    }
}