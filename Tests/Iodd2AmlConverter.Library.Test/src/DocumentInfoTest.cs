using System.Xml.Linq;
using Iodd2AmlConverter.Library.Iodd.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test
{

    [TestClass]
    public class DocumentInfoTest
    {

        private const string XmlText =
            @"<DocumentInfo version=""V2.3.2"" releaseDate=""2014-09-09"" copyright=""Copyright 2014, ifm electronic gmbh"" />";

        private DocumentInfo Info { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Info = new DocumentInfo();
            Info.Deserialize(XElement.Parse(XmlText));
        }

        [TestMethod]
        public void HasCorrectVersion()
        {
            Assert.AreEqual("V2.3.2", Info.Version);
        }

        [TestMethod]
        public void HasCorrectCopyright()
        {
            Assert.AreEqual("Copyright 2014, ifm electronic gmbh", Info.Copyright);
        }

        [TestMethod]
        public void HasCorrectReleaseDate()
        {
            Assert.AreEqual("2014-09-09", Info.ReleaseDate);
        }

        [TestMethod]
        public void IsCorrectAml()
        {
            var amlCollection = Info.ToAml();
            Assert.IsNotNull(amlCollection);
        }

    }

}