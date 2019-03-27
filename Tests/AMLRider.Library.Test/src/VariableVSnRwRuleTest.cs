using System;
using System.Linq;
using System.Xml.Linq;
using AMLRider.Library.Extensions;
using AMLRider.Library.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMLRider.Library.Test
{
    [TestClass]
    public class VariableVSnRwRuleTest
    {
        private const string VariableId = "V_SN_RW";
        private const string VariableIndex = "84";
        private const string AccessRights = "rw";
        private const string Datatype = "StringT";
        private const string FixedLength = "16";
        private const string EncodingType = "UTF-8";
        private const string NameTextId = "TI_SN_RW";
        private const string DescriptionTextId = "TI_SN_RW_Descr";
        
        private XElement XmlSampleElement { get; set; }
        
        private XElement XmlSampleElementWrong { get; set; }
        
        [TestInitialize]
        public void Initialize()
        {
            var sample = $"<TestWrapper xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><Variable id=\"{VariableId}\" index=\"{VariableIndex}\" accessRights=\"{AccessRights}\">\n\t<Datatype xsi:type=\"{Datatype}\" fixedLength=\"{FixedLength}\" encoding=\"{EncodingType}\"/>\n\t<Name textId=\"{NameTextId}\" />\n\t<Description textId=\"{DescriptionTextId}\" />\n</Variable></TestWrapper>";
            const string wrongSample = @"<TestWrapper xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""><SomeVariable id=""V_SN_RW"" index=""84"" accessRights=""rw""><Datatype xsi:type=""StringT"" fixedLength=""16"" encoding=""UTF-8""/><Name textId=""TI_SN_RW"" /><Description textId=""TI_SN_RW_Descr"" /></SomeVariable></TestWrapper>";
                        
            XmlSampleElement = XElement.Parse(sample).Element("Variable");
            XmlSampleElementWrong = XElement.Parse(wrongSample).Element("SomeVariable");
        }
        
        [TestMethod]
        public void CanApplyRuleReturnsTrue()
        {
            var rule = new VariableVSnRwRule();

            var result = rule.CanApplyRule(XmlSampleElement);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanApplyRuleReturnsFalse()
        {
            var rule = new VariableVSnRwRule();
            
            var result = rule.CanApplyRule(XmlSampleElementWrong);
            Assert.IsFalse(result);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanApplyRuleThrowsException()
        {
            var rule = new VariableVSnRwRule();
            rule.Apply(XmlSampleElementWrong);
        }

        [TestMethod]
        public void IsRootElementInternalElement()
        {
            var rule = new VariableVSnRwRule();

            var element = rule.Apply(XmlSampleElement);
            Assert.AreEqual("InternalElement", element.Name);
        }
        
        [TestMethod]
        public void HasCorrectSubElements()
        {
            var rule = new VariableVSnRwRule();

            var element = rule.Apply(XmlSampleElement);
            var subElementCount = element.Elements().Count();
            var subElement1 = element.Elements().First();
            var subElement2 = element.Elements().Skip(1).First();
            var subElement3 = element.Elements().Skip(2).First();
            var subElement4 = element.Elements().Skip(3).First();
            var subElement5 = element.Elements().Skip(4).First();
            var subElement6 = element.Elements().Skip(5).First();
            
            Assert.AreEqual(6, subElementCount);
            Assert.AreEqual(subElement1.Name, "Attribute");
            Assert.AreEqual(subElement2.Name, "Attribute");
            Assert.AreEqual(subElement3.Name, "Attribute");
            Assert.AreEqual(subElement4.Name, "Attribute");
            Assert.AreEqual(subElement5.Name, "Attribute");
            Assert.AreEqual(subElement6.Name, "Description");
        }
        
        [TestMethod]
        public void IsSchemaInstanceSetCorrectly()
        {
            var rule = new VariableVSnRwRule();

            var amlElement = rule.Apply(XmlSampleElement);
            var variableId = amlElement.GetAttributeValue("ID");
            var variableIndex = amlElement.Element("Attribute")?.Element("Value")?.Value;
            var accessRights = amlElement.Elements().Skip(1).First().Element("Value")?.Value;
            var fixedLength = amlElement.Elements().Skip(2).First().Element("Value")?.Value;
            var encodingType = amlElement.Elements().Skip(3).First().Element("Value")?.Value;
            var nameTextId = amlElement.Elements().Skip(4).First().GetAttributeValue("Name");
            var descriptionTextId = amlElement.Elements().Skip(5).First().GetAttributeValue("textId");
            
            Assert.AreEqual(VariableId, variableId);
            Assert.AreEqual(VariableIndex, variableIndex);
            Assert.AreEqual(AccessRights, accessRights);
            Assert.AreEqual(FixedLength, fixedLength);
            Assert.AreEqual(EncodingType, encodingType);
            Assert.AreEqual(NameTextId, nameTextId);
            Assert.AreEqual(DescriptionTextId, descriptionTextId);
        }
    }
}