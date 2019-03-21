using System;
using System.Linq;
using System.Xml.Linq;
using AMLRider.Library.Extensions;
using AMLRider.Library.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMLRider.Library.Test
{
    [TestClass]
    public class VariableRecordTRuleTest
    {
        private const string VariableId = "V_Inv_Record";
        private const string Index = "64";
        private const string SubIndexAccessSupported = "true";
        private const string SubIndex = "1";
        private const string AccessRights = "rw";
        private const string BitLength = "16";
        private const string BitOffset = "8";
        private const string DataTypeId = "DT_Inversion";
        private const string Name = "TI_VAR_Inversion";
        private const string VariableName = "TI_VAR_Inversion_01";
        private const string DescriptionId = "TI_VAR_Inversion_Descr";
        private XElement[] _childNodes = new XElement[0];
        
        private XElement XmlSampleElement { get; set; }
        
        private XElement XmlSampleElementWrong { get; set; }
        
        [TestInitialize]
        public void Initialize()
        {
            var sample = $"<TestWrapper xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><Variable id=\"{VariableId}\" index=\"{Index}\" accessRights=\"{AccessRights}\">\n\t<Datatype xsi:type=\"RecordT\" bitLength=\"{BitLength}\" subindexAccessSupported=\"{SubIndexAccessSupported}\">\n\t\t<RecordItem subindex=\"{SubIndex}\" bitOffset=\"{BitOffset}\">\n\t\t\t<DatatypeRef datatypeId=\"{DataTypeId}\" />\n\t\t\t<Name textId=\"{VariableName}\" />\n\t\t</RecordItem>\n\t</Datatype>\n\t<Name textId=\"{Name}\" />\n\t<Description textId=\"{DescriptionId}\" />\n</Variable></TestWrapper>";
            const string wrongSample = @"<TestWrapper xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance\""><SomeVariable id=""V_Inv_Record"" index=""64"" accessRights=""rw""><Datatype xsi:type=""RecordT"" bitLength=""16"" subindexAccessSupported=""true""><RecordItem subindex=""1"" bitOffset=""8""><DatatypeRef datatypeId=""DT_Inversion"" /><Name textId=""TI_VAR_Inversion_01"" /></RecordItem></Datatype><Name textId=""TI_VAR_Inversion"" /><Description textId=""TI_VAR_Inversion_Descr"" /></SomeVariable></TestWrapper>";

            XmlSampleElement = XElement.Parse(sample);
            XmlSampleElementWrong = XElement.Parse(wrongSample);
        }
        
        [TestMethod]
        public void CanApplyRuleReturnsTrue()
        {
            var rule = new VariableRecordTRule();

            var result = rule.CanApplyRule(XmlSampleElement.Element("Variable"));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanApplyRuleReturnsFalse()
        {
            var rule = new VariableRecordTRule();
            
            var result = rule.CanApplyRule(XmlSampleElementWrong.Element("SomeVariable"));
            Assert.IsFalse(result);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanApplyRuleThrowsException()
        {
            var rule = new VariableRecordTRule();
            rule.Apply(XmlSampleElementWrong.Element("SomeVariable"));
        }

        [TestMethod]
        public void IsRootElementInternalElement()
        {
            var rule = new VariableRecordTRule();

            var element = rule.Apply(XmlSampleElement.Element("Variable"));
            Assert.AreEqual("InternalElement", element.Name);
        }
        
        [TestMethod]
        public void HasCorrectSubElements()
        {
            var rule = new VariableRecordTRule();

            var element = rule.Apply(XmlSampleElement.Element("Variable"));
            var subElementCount = element.Elements().Count();
            var subElement1 = element.Elements().First();
            var subElement2 = element.Elements().Skip(1).First();
            var subElement3 = element.Elements().Skip(2).First();
            var subElement4 = element.Elements().Skip(3).First();
            var subElement5 = element.Elements().Skip(4).First();
            
            Assert.AreEqual(5, subElementCount);
            Assert.AreEqual(subElement1.Name, "Attribute");
            Assert.AreEqual(subElement2.Name, "Attribute");
            Assert.AreEqual(subElement3.Name, "Attribute");
            Assert.AreEqual(subElement4.Name, "Attribute");
            Assert.AreEqual(subElement5.Name, "InternalElement");

        }
        
        [TestMethod]
        public void AreParamsSetCorrectly()
        {
            var rule = new VariableRecordTRule();

            var amlElement = rule.Apply(XmlSampleElement.Element("Variable"));
            var variableId = amlElement.GetAttributeValue("Name");
            
            Assert.AreEqual(VariableId, variableId);
        }
    }
}