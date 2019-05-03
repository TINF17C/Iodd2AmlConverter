using System;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test
{
 
    [TestClass]
    public class ProfileHeaderRuleTest
    {
        
        private XElement XmlSampleElement { get; set; }
        
        private XElement XmlSampleElementWrong { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            const string sampleXml =
                "<ProfileHeader> \n\t<ProfileIdentification>IO Device Profile</ProfileIdentification>\n\t<ProfileRevision>1.1</ProfileRevision> \n\t<ProfileName>Device Profile for IO Devices</ProfileName>\n\t<ProfileSource>IO-Link Consortium</ProfileSource>\n\t<ProfileClassID>Device</ProfileClassID> \n\t<ISO15745Reference>  \n\t\t<ISO15745Part>1</ISO15745Part>\n\t\t<ISO15745Edition>1</ISO15745Edition> \n\t\t<ProfileTechnology>IODD</ProfileTechnology> \n\t</ISO15745Reference> \n</ProfileHeader>\n";
            const string sampleXmlWrong = "<SomeHeader></SomeHeader>";
            
            XmlSampleElement = XElement.Parse(sampleXml);
            XmlSampleElementWrong = XElement.Parse(sampleXmlWrong);
        }
        
        [TestMethod]
        public void CanApplyRuleReturnsTrue()
        {
            var rule = new ProfileHeaderRule();

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
            var rule = new ProfileHeaderRule();
            rule.Apply(XmlSampleElementWrong);
        }

        [TestMethod]
        public void IsRootElementInternalElement()
        {
            var rule = new ProfileHeaderRule();

            var element = rule.Apply(XmlSampleElement);
            Assert.AreEqual("InternalElement", element.Name);
        }
        
    }
    
}