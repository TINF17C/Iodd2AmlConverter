using System.Xml.Linq;
using Iodd2AmlConverter.Library.Iodd.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test
{

    [TestClass]
    public class VariableCollectionTest
    {

        private const string XmlText =
            @"<VariableCollection>
	            <StdVariableRef />
	            <Variable />
            </VariableCollection>";
        
        private VariableCollection Collection { get; set; }
        
        [TestInitialize]
        public void Initialize()
        {
            Collection = new VariableCollection();
            Collection.Deserialize(XElement.Parse(XmlText));
        }
        
        [TestMethod]
        public void HasStdVariableRefs()
        {
            Assert.IsTrue(Collection.StdVariableRefs.Count > 0);
        }
        
        [TestMethod]
        public void HasProfileHeader()
        {
            Assert.IsTrue(Collection.Variables.Count > 0);
        }

        [TestMethod]
        public void IsCorrectAml()
        {
            var amlCollection = Collection.ToAml();
            Assert.IsNotNull(amlCollection);
        }

    }

}