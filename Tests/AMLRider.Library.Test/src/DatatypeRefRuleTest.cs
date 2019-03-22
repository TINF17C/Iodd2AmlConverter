using System;
using System.Linq;
using System.Xml.Linq;
using AMLRider.Library.Extensions;
using AMLRider.Library.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMLRider.Library.Test
{
    [TestClass]
    public class DatatypeRefRuleTest
    {
        private const string DataTypeId = "DT_Inversion";
        
        private XElement XmlSampleElement { get; set; }
        
        private XElement XmlSampleElementWrong { get; set; }
        
        [TestInitialize]
        public void Initialize()
        {
            var sample = $"<DatatypeRef datatypeId=\"{DataTypeId}\" />";
            const string wrongSample = @"<SometypeRef datatypeId=""DT_Inversion"" />";
            
            
            XmlSampleElement = XElement.Parse(sample);
            XmlSampleElementWrong = XElement.Parse(wrongSample);
        }
        
        [TestMethod]
        public void CanApplyRuleReturnsTrue()
        {
            var rule = new DatatypeRefRule();

            var result = rule.CanApplyRule(XmlSampleElement);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanApplyRuleReturnsFalse()
        {
            var rule = new DatatypeRefRule();
            
            var result = rule.CanApplyRule(XmlSampleElementWrong);
            Assert.IsFalse(result);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanApplyRuleThrowsException()
        {
            var rule = new DatatypeRefRule();
            rule.Apply(XmlSampleElementWrong);
        }

        [TestMethod]
        public void IsRootElementAttribute()
        {
            var rule = new DatatypeRefRule();

            var element = rule.Apply(XmlSampleElement);
            Assert.AreEqual("Attribute", element.Name);
        }
        
        [TestMethod]
        public void HasCorrectSubElements()
        {
            var rule = new DatatypeRefRule();

            var element = rule.Apply(XmlSampleElement);
            var subElementCount = element.Elements().Count();
            var subElement = element.Elements().First();
            var subElement2 = element.Elements().Skip(1).First();
            var subElement3 = element.Elements().Skip(2).First();
            
            Assert.AreEqual(3, subElementCount);
            Assert.AreEqual(subElement.Name, "RefSemantic");
            Assert.AreEqual(subElement2.Name, "Attribute");
            Assert.AreEqual(subElement3.Name, "Attribute");

        }
        
        [TestMethod]
        public void IsDataTypeIdSetCorrectly()
        {
            var rule = new DatatypeRefRule();

            var dataTypeRefName = rule.Apply(XmlSampleElement).GetAttributeValue("Name");
            
            Assert.AreEqual(DataTypeId, dataTypeRefName);
        }
    }
}