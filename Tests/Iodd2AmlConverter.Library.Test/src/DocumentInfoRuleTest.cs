using System;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test
{
    
    [TestClass]
    public class DocumentInfoRuleTest
    {

        private const string CopyrightText = "Copyright 2016, Balluff GmbH";
        private const string ReleaseDateText = "2016-11-16";
        private const string VersionText = "V1.1.0.2";
        
        private XElement XmlSampleElement { get; set; }
        
        private XElement XmlSampleElementWrong { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            var sample = $"<DocumentInfo copyright=\"{CopyrightText}\" releaseDate=\"{ReleaseDateText}\" version=\"{VersionText}\" />";
            const string wrongSample = @"<SomeInfo copyright=""Copyright 2016, Balluff GmbH"" releaseDate=""2016-11-16"" version=""V1.1.0.2"" />";
            
            
            XmlSampleElement = XElement.Parse(sample);
            XmlSampleElementWrong = XElement.Parse(wrongSample);
        }
        
        [TestMethod]
        public void CanApplyRuleReturnsTrue()
        {
            var rule = new DocumentInfoRule();

            var result = rule.CanApplyRule(XmlSampleElement);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanApplyRuleReturnsFalse()
        {
            var rule = new DocumentInfoRule();

            var result = rule.CanApplyRule(XmlSampleElementWrong);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanApplyRuleThrowsException()
        {
            var rule = new DocumentInfoRule();
            rule.Apply(XmlSampleElementWrong);
        }

        [TestMethod]
        public void IsRootElementAdditionalInformation()
        {
            var rule = new DocumentInfoRule();

            var element = rule.Apply(XmlSampleElement);
            Assert.AreEqual("AdditionalInformation", element.Name);
        }
        
        [TestMethod]
        public void HasWriterHeaderElementOnly()
        {
            var rule = new DocumentInfoRule();

            var element = rule.Apply(XmlSampleElement);
            var subElementCount = element.Elements().Count();
            var subElement = element.Elements().First();
            
            Assert.AreEqual(1, subElementCount);
            Assert.AreEqual(subElement.Name, "WriterHeader");
        }
        
        [TestMethod]
        public void IsCopyrightSetCorrectly()
        {
            var rule = new DocumentInfoRule();

            var writerHeader = rule.Apply(XmlSampleElement).Element("WriterHeader");
            var vendorElement = writerHeader?.Element("WriterVendor")?.Value;
            
            Assert.AreEqual(CopyrightText, vendorElement);
        }
        
        [TestMethod]
        public void IsReleaseDateSetCorrectly()
        {
            var rule = new DocumentInfoRule();

            var writerHeader = rule.Apply(XmlSampleElement).Element("WriterHeader");
            var releaseElement = writerHeader?.Element("WriterRelease")?.Value;
            
            Assert.AreEqual(ReleaseDateText, releaseElement);
        }
        
        [TestMethod]
        public void IsVersionSetCorrectly()
        {
            var rule = new DocumentInfoRule();

            var writerHeader = rule.Apply(XmlSampleElement).Element("WriterHeader");
            var versionElement = writerHeader?.Element("WriterVersion")?.Value;
            
            Assert.AreEqual(VersionText, versionElement);
        }
        
    }
    
}