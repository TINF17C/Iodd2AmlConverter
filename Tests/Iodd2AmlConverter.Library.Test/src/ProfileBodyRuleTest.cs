using System;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test
{
    
    [TestClass]
    public class ProfileBodyRuleTest
    {
        
        private XElement XmlSampleElement { get; set; }
        
        private XElement XmlSampleElementWrong { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            const string sample = "<ProfileBody></ProfileBody>";
            const string wrongSample = "<OtherBody></OtherBody>";
            
            
            XmlSampleElement = XElement.Parse(sample);
            XmlSampleElementWrong = XElement.Parse(wrongSample);
        }
        
        [TestMethod]
        public void CanApplyRuleReturnsTrue()
        {
            var rule = new ProfileBodyRule();

            var result = rule.CanApplyRule(XmlSampleElement);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanApplyRuleReturnsFalse()
        {
            var rule = new ProfileBodyRule();

            var result = rule.CanApplyRule(XmlSampleElementWrong);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanApplyRuleThrowsException()
        {
            var rule = new ProfileBodyRule();
            rule.Apply(XmlSampleElementWrong);
        }

        [TestMethod]
        public void IsRootElementSystemUnitClassLib()
        {
            var rule = new ProfileBodyRule();

            var element = rule.Apply(XmlSampleElement);
            Assert.AreEqual("SystemUnitClassLib", element.Name);
        }
        
        [TestMethod]
        public void IsSubElementSystemUnitClass()
        {
            var rule = new ProfileBodyRule();

            var element = rule.Apply(XmlSampleElement);
            var result = element.Element("SystemUnitClass");
            
            Assert.IsNotNull(result);
        }
        
    }
    
}