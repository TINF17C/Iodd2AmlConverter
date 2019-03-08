using System.Xml.Linq;
using AMLRider.Library.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMLRider.Library.Test
{
    
    [TestClass]
    public class DocumentInfoRuleTest
    {
        
        private XElement XmlSampleElement { get; set; }
        
        private XElement XmlSampleElementWrong { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            const string sample = @"<DocumentInfo copyright=""Copyright 2016, Balluff GmbH"" releaseDate=""2016-11-16"" version=""V1.1.0.2"" />";
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
        
    }
    
}