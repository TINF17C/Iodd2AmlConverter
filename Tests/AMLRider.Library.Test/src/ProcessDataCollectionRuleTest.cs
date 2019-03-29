using System;
using System.Linq;
using System.Xml.Linq;
using AMLRider.Library.Extensions;
using AMLRider.Library.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMLRider.Library.Test
{
    
    [TestClass]
    public class ProcessDataCollectionRuleTest
    {

        private const string ProcessDataInIdText = "V_Pd_InT";
        private const string TextIdText = "TI_PD";
        private const string SubIndexText = "1";
        private const string BitOffsetText = "8";
        
        private XElement XmlSampleElement { get; set; }
        
        private XElement XmlSampleElementWrong { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            var sample = $"<TestWrapper xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><ProcessDataCollection>\n\t<ProcessData id=\"V_PdT\">\n\t\t<ProcessDataIn id=\"{ProcessDataInIdText}\" bitLength=\"16\">\n\t\t\t<Datatype xsi:type=\"RecordT\" bitLength=\"16\">\n\t\t\t\t<RecordItem subindex=\"{SubIndexText}\" bitOffset=\"{BitOffsetText}\">\n\t\t\t\t\t<DatatypeRef datatypeId=\"DT_DigitalIn\" />\n\t\t\t\t\t<Name textId=\"TI_PD_Switchstate_01\" />\n\t\t\t\t</RecordItem>\n\t\t\t</Datatype>\n\t\t\t<Name textId=\"{TextIdText}\" />\n\t\t</ProcessDataIn>\n\t</ProcessData>\n</ProcessDataCollection>\n</TestWrapper>";
            const string wrongSample = "<TestWrapper xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\n<SomeDataCollection>\n\t<ProcessData id=\"V_PdT\">\n\t\t<ProcessDataIn id=\"V_Pd_InT\" bitLength=\"16\">\n\t\t\t<Datatype xsi:type=\"RecordT\" bitLength=\"16\">\n\t\t\t\t<RecordItem subindex=\"1\" bitOffset=\"8\">\n\t\t\t\t\t<DatatypeRef datatypeId=\"DT_DigitalIn\" />\n\t\t\t\t\t<Name textId=\"TI_PD_Switchstate_01\" />\n\t\t\t\t</RecordItem>\n\t\t\t</Datatype>\n\t\t\t<Name textId=\"TI_PD\" />\n\t\t</ProcessDataIn>\n\t</ProcessData>\n</SomeDataCollection>\n</TestWrapper>";
            
            
            XmlSampleElement = XElement.Parse(sample).Element("ProcessDataCollection");
            XmlSampleElementWrong = XElement.Parse(wrongSample).Element("SomeDataCollection");
        }
        
        [TestMethod]
        public void CanApplyRuleReturnsTrue()
        {
            var rule = new ProcessDataCollectionRule();

            var result = rule.CanApplyRule(XmlSampleElement);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanApplyRuleReturnsFalse()
        {
            var rule = new ProcessDataCollectionRule();

            var result = rule.CanApplyRule(XmlSampleElementWrong);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanApplyRuleThrowsException()
        {
            var rule = new ProcessDataCollectionRule();
            rule.Apply(XmlSampleElementWrong);
        }

        [TestMethod]
        public void IsRootElementInternalElement()
        {
            var rule = new ProcessDataCollectionRule();

            var element = rule.Apply(XmlSampleElement);
            Assert.AreEqual("InternalElement", element.Name);
        }

        [TestMethod]
        public void IsProcessDataInIdSetCorrectly()
        {
            var rule = new ProcessDataCollectionRule();

            var element = rule.Apply(XmlSampleElement);
            var nameValue = element.GetAttributeValue("Name");
            var idValue = element.GetAttributeValue("ID");
            
            Assert.AreEqual(ProcessDataInIdText, nameValue);
            Assert.AreEqual(ProcessDataInIdText, idValue);
        }
        
        [TestMethod]
        public void IsTextIdSetCorrectly()
        {
            var rule = new ProcessDataCollectionRule();

            var element = rule.Apply(XmlSampleElement);
            var subElement = element.Element("InternalElement");
            var nameValue = subElement.GetAttributeValue("Name");
            
            Assert.AreEqual(TextIdText, nameValue);
        }

        [TestMethod]
        public void IsRecordItemSubIndexSetCorrectly()
        {
            var rule = new ProcessDataCollectionRule();

            var element = rule.Apply(XmlSampleElement);
            var subElement = element
                .Element("InternalElement")?
                .Element("InternalElement");

            var value = GetAttributeValue(subElement, "subindex");
            Assert.AreEqual(SubIndexText, value);
        }

        [TestMethod]
        public void IsRecordItemBitOffsetSetCorrectly()
        {
            var rule = new ProcessDataCollectionRule();

            var element = rule.Apply(XmlSampleElement);
            var subElement = element
                .Element("InternalElement")?
                .Element("InternalElement");

            var value = GetAttributeValue(subElement, "bitOffset");
            Assert.AreEqual(BitOffsetText, value);
        }
        
        private static string GetAttributeValue(XContainer element, string attributeName)
        {
            return element
                .Elements("Attribute")
                .FirstOrDefault(x => x.Attribute("Name")?.Value == attributeName)?
                .Element("Value")?
                .Value;
        }
        
    }
    
}