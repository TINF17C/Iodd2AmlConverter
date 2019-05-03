using System;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test
{
    
    [TestClass]
    public class RuleSelectorTest
    {
        
        private XElement DatatypeRefElement { get; set; }
        
        private XElement DeviceIdentityElement { get; set; }
        
        private XElement EventCollectionElement { get; set; }
        
        private XElement DocumentInfoElement { get; set; }
        
        private XElement RandomElement { get; set; }
        
        [TestInitialize]
        public void Initialize()
        {
            DatatypeRefElement = XElement.Parse("<DatatypeRef datatypeId=\"DT_Inversion\" />");
            DeviceIdentityElement = XElement.Parse("<DeviceIdentity vendorId=\"888\" vendorName=\"BALLUFF\" deviceId=\"262658\">\n\t<VendorText textId=\"TI_VendorText\" />\n\t<VendorUrl textId=\"TI_VendorUrl\" />\n\t<VendorLogo name=\"Balluff-logo.png\" />\n\t<DeviceName textId=\"TI_DeviceName\" />\n\t<DeviceFamily textId=\"TI_DeviceFamily\" />\n</DeviceIdentity>\n");
            EventCollectionElement = XElement.Parse("<EventCollection>\n\t<StdEventRef code=\"20753\" />\n\t<StdEventRef code=\"20754\" />\n\t<StdEventRef code=\"30480\" />\n</EventCollection>\n");
            DocumentInfoElement = XElement.Parse("<DocumentInfo copyright=\"Copyright 2016, Balluff GmbH\" releaseDate=\"2016-11-16\" version=\"V1.1.0.2\" />");
            
            RandomElement = XElement.Parse("<RandomElement Id=\"1\"><Value>130</Value></RandomElement>");
        }

        [TestMethod]
        public void SelectsDatatypeRefRule()
        {
            var ruleSelector = new RuleSelector();
            var selectedRule = ruleSelector.SelectRule(DatatypeRefElement);
            
            Assert.IsInstanceOfType(selectedRule, typeof(DatatypeRefRule));
        }
        
        [TestMethod]
        public void SelectsDeviceIdentityRule()
        {
            var ruleSelector = new RuleSelector();
            var selectedRule = ruleSelector.SelectRule(DeviceIdentityElement);
            
            Assert.IsInstanceOfType(selectedRule, typeof(DeviceIdentityRule));
        }
        
        [TestMethod]
        public void SelectsEventCollectionRule()
        {
            var ruleSelector = new RuleSelector();
            var selectedRule = ruleSelector.SelectRule(EventCollectionElement);
            
            Assert.IsInstanceOfType(selectedRule, typeof(EventCollectionRule));
        }
        
        [TestMethod]
        public void SelectsDocumentInfoRule()
        {
            var ruleSelector = new RuleSelector();
            var selectedRule = ruleSelector.SelectRule(DocumentInfoElement);
            
            Assert.IsInstanceOfType(selectedRule, typeof(DocumentInfoRule));
        }

        [TestMethod]
        public void ReturnsNullOnNoFittingRule()
        {
            var ruleSelector = new RuleSelector();
            var selectedRule = ruleSelector.SelectRule(RandomElement);
            
            Assert.AreEqual((object) null, selectedRule);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ThrowsExceptionOnMultipleRules()
        {
            // TODO:
            throw new InvalidOperationException();
        }
        
    }
    
}
