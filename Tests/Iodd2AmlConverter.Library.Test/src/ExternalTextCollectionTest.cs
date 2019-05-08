using System.Xml.Linq;
using Iodd2AmlConverter.Library.Iodd.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test
{

    [TestClass]
    public class ExternalTextCollectionTest
    {

        private const string XmlText =
            @"<ExternalTextCollection>
                <PrimaryLanguage xml:lang=""en"" />
                <Language xml:lang=""de"" />
            </ExternalTextCollection>";
        
        private ExternalTextCollection TextCollection { get; set; }
        
        [TestInitialize]
        public void Initialize()
        {
            TextCollection = new ExternalTextCollection();
            TextCollection.Deserialize(XElement.Parse(XmlText));
        }
        
        [TestMethod]
        public void HasPrimaryLanguage()
        {
            Assert.IsNotNull(TextCollection.PrimaryLanguage);
        }
        
        [TestMethod]
        public void HasLanguages()
        {
            Assert.IsNotNull(TextCollection.Languages);
            Assert.IsTrue(TextCollection.Languages.Count > 0);
        }

        [TestMethod]
        public void IsCorrectAml()
        {
            var amlCollection = TextCollection.ToAml();
            Assert.IsNotNull(amlCollection);
        }

    }

}