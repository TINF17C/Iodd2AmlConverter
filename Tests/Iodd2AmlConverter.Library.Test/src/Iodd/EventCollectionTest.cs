using System.Xml.Linq;
using Iodd2AmlConverter.Library.Iodd.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test.Iodd
{

    [TestClass]
    public class EventCollectionTest
    {

        private const string XmlText =
            @"<EventCollection>
	            <Event />
                <StdEventRef />
            </EventCollection>";
        
        private EventCollection Collection { get; set; }
        
        [TestInitialize]
        public void Initialize()
        {
            Collection = new EventCollection();
            Collection.Deserialize(XElement.Parse(XmlText));
        }
        
        [TestMethod]
        public void HasEvents()
        {
            Assert.IsTrue(Collection.Events.Count > 0);
        }
        
        [TestMethod]
        public void HasStdEventRefs()
        {
            Assert.IsTrue(Collection.StdEventRefs.Count > 0);
        }

        [TestMethod]
        public void IsCorrectAml()
        {
            var amlCollection = Collection.ToAml();
            Assert.IsNotNull(amlCollection);
        }

    }

}