using System.Xml.Linq;
using Iodd2AmlConverter.Library.Iodd.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test
{

    [TestClass]
    public class TextTest
    {

        private const string XmlText =
            @"<Text id=""TI_Product1_Name"" value=""PI2796"" />";
        
        private Text Text { get; set; }
        
        [TestInitialize]
        public void Initialize()
        {
            Text = new Text();
            Text.Deserialize(XElement.Parse(XmlText));
        }
        
        [TestMethod]
        public void HasCorrectTextId()
        {
            Assert.AreEqual("TI_Product1_Name", Text.Id);
        }
        
        [TestMethod]
        public void HasCorrectValue()
        {
            Assert.AreEqual("PI2796", Text.Value);
        }
        
        [TestMethod]
        public void IsCorrectAml()
        {
            var amlCollection = Text.ToAml();
            Assert.IsNotNull(amlCollection);
        }

    }

}