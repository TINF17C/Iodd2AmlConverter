using System;
using System.Linq;
using System.Xml.Linq;
using AMLRider.Library.Extensions;
using AMLRider.Library.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMLRider.Library.Test
{
    
    [TestClass]
    public class StdVariableRefRuleTest
    {

        private const string IdText = "V_SystemCommand";
        private const string ValueText = "130";
        
        private XElement XmlSampleElement { get; set; }
        
        private XElement XmlSampleElementWrong { get; set; }
        
        [TestInitialize]
        public void Initialize()
        {
            var sample = $"<StdVariableRef id=\"{IdText}\">\n\t<StdSingleValueRef value=\"{ValueText}\" />\n</StdVariableRef>\n";
            var wrongSample = $"<StdSomeRef id=\"{IdText}\">\n\t<StdSingleValueRef value=\"{ValueText}\" />\n</StdSomeRef>\n";
            
            XmlSampleElement = XElement.Parse(sample);
            XmlSampleElementWrong = XElement.Parse(wrongSample);
        }
        
        [TestMethod]
        public void CanApplyRuleReturnsTrue()
        {
            var rule = new StdVariableRefRule();

            var result = rule.CanApplyRule(XmlSampleElement);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanApplyRuleReturnsFalse()
        {
            var rule = new StdVariableRefRule();

            var result = rule.CanApplyRule(XmlSampleElementWrong);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanApplyRuleThrowsException()
        {
            var rule = new StdVariableRefRule();
            rule.Apply(XmlSampleElementWrong);
        }

        [TestMethod]
        public void IsRootElementInternalElement()
        {
            var rule = new StdVariableRefRule();

            var element = rule.Apply(XmlSampleElement);
            Assert.AreEqual("InternalElement", element.Name);
        }

        [TestMethod]
        public void IsIdAndNameSetCorrectly()
        {
            var rule = new StdVariableRefRule();

            var element = rule.Apply(XmlSampleElement);
            var id = element.GetAttributeValue("ID");
            var name = element.GetAttributeValue("Name");
            
            Assert.AreEqual(IdText, id);
            Assert.AreEqual(IdText, name);
        }

        [TestMethod]
        public void IsValueSetCorrectly()
        {
            var rule = new StdVariableRefRule();

            var element = rule.Apply(XmlSampleElement);
            var value = element
                .Elements()
                .FirstOrDefault(x => x.Name == "Attribute")?
                .Element("Value")?
                .Value;
            
            Assert.AreEqual(ValueText, value);
        }
        
    }
    
}