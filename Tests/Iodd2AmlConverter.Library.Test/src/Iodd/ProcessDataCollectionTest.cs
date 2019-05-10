using System.Xml.Linq;
using Iodd2AmlConverter.Library.Iodd.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test.Iodd
{

    [TestClass]
    public class ProcessDataCollectionTest
    {

        private const string XmlText =
            @"<ProcessDataCollection>
	            <ProcessData />
            </ProcessDataCollection>";

        private ProcessDataCollection Collection { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Collection = new ProcessDataCollection();
            Collection.Deserialize(XElement.Parse(XmlText));
        }

        [TestMethod]
        public void HasProcessData()
        {
            Assert.IsTrue(Collection.ProcessDataList.Count > 0);
        }

        [TestMethod]
        public void IsCorrectAml()
        {
            var amlCollection = Collection.ToAml();
            Assert.IsNotNull(amlCollection);
        }

    }

}