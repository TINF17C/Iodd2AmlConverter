using System.Xml.Linq;
using Iodd2AmlConverter.Library.Iodd.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test.Iodd
{

    [TestClass]
    public class EventTest
    {

        private const string XmlText =
            @"<Event code=""6160"" type=""Error"" mode=""AppearDisappear"">
	            <Name textId=""TI_Ev1810"" />
	            <Description textId=""TI_EvDescr_x"" />
            </Event>";
        
        private Event Event { get; set; }
        
        [TestInitialize]
        public void Initialize()
        {
            Event = new Event();
            Event.Deserialize(XElement.Parse(XmlText));
        }
        
        [TestMethod]
        public void HasCorrectCode()
        {
            Assert.AreEqual(6160, Event.Code);
        }
        
        [TestMethod]
        public void HasCorrectType()
        {
            Assert.AreEqual("Error", Event.Type);
        }
        
        [TestMethod]
        public void HasCorrectName()
        {
            Assert.AreEqual("TI_Ev1810", Event.Name);
        }
        
        [TestMethod]
        public void HasCorrectDescription()
        {
            Assert.AreEqual("TI_EvDescr_x", Event.Description);
        }
        
        [TestMethod]
        public void IsCorrectAml()
        {
            var amlCollection = Event.ToAml();
            Assert.IsNotNull(amlCollection);
        }

    }

}