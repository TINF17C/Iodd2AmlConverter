using System.Xml.Linq;
using Iodd2AmlConverter.Library.Iodd.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test
{

    [TestClass]
    public class LanguageTest
    {

        private const string XmlText =
            @"<Language xml:lang=""en"" />";
        
        private Language Language { get; set; }
        
        [TestInitialize]
        public void Initialize()
        {
            Language = new Language();
            Language.Deserialize(XElement.Parse(XmlText));
        }
        
        [TestMethod]
        public void HasCorrectLanguageId()
        {
            Assert.AreEqual("en", Language.Lang);
        }
        
        [TestMethod]
        public void IsCorrectAml()
        {
            var amlCollection = Language.ToAml();
            Assert.IsNotNull(amlCollection);
        }

    }

}