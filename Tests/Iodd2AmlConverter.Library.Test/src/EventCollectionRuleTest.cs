using System;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test
{
    [TestClass]
    public class EventCollectionRuleTest
    {
        
        private const string EventCode1 = "20753";
        private const string EventCode2 = "20754";
        private const string EventCode3 = "30480";

        private static readonly string[] EventCodes = {EventCode1, EventCode2, EventCode3};

        private XElement XmlSampleElement { get; set; }

        private XElement XmlSampleElementWrong { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            var sample =
                $"<EventCollection>\n\t<StdEventRef code=\"{EventCode1}\" />\n\t<StdEventRef code=\"{EventCode2}\" />\n\t<StdEventRef code=\"{EventCode3}\" />\n</EventCollection>\n";
            var wrongSample =
                $"<OtherCollection>\n\t<StdEventRef code=\"{EventCode1}\" />\n\t<StdEventRef code=\"{EventCode2}\" />\n\t<StdEventRef code=\"{EventCode3}\" />\n</OtherCollection>\n";


            XmlSampleElement = XElement.Parse(sample);
            XmlSampleElementWrong = XElement.Parse(wrongSample);
        }

        [TestMethod]
        public void CanApplyRuleReturnsTrue()
        {
            var rule = new EventCollectionRule();

            var result = rule.CanApplyRule(XmlSampleElement);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanApplyRuleReturnsFalse()
        {
            var rule = new EventCollectionRule();

            var result = rule.CanApplyRule(XmlSampleElementWrong);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanApplyRuleThrowsException()
        {
            var rule = new EventCollectionRule();
            rule.Apply(XmlSampleElementWrong);
        }

        [TestMethod]
        public void IsRootElementInternalElement()
        {
            var rule = new EventCollectionRule();

            var element = rule.Apply(XmlSampleElement);
            Assert.AreEqual("InternalElement", element.Name);
        }

        [TestMethod]
        public void HasRuleElementCorrectNameAttribute()
        {
            var rule = new EventCollectionRule();

            var element = rule.Apply(XmlSampleElement);
            var nameAttributeValue = element.GetAttributeValue("Name");

            Assert.AreEqual("EventCollection", nameAttributeValue);
        }

        [TestMethod]
        public void AreEventCodesCorrectlySet()
        {
            var rule = new EventCollectionRule();

            var element = rule.Apply(XmlSampleElement);
            var attributeElements = element.Elements("Attribute");

            var result = EventCodes.SequenceEqual(
                attributeElements.Select(e => e.GetAttributeValue("Name"))
            );
            
            Assert.IsTrue(result);
        }
        
    }
}