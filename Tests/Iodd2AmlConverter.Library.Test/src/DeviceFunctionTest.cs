using System.Xml.Linq;
using Iodd2AmlConverter.Library.Iodd.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Library.Test
{

    [TestClass]
    public class DeviceFunctionTest
    {

        private const string XmlText =
            @"<DeviceFunction>
	            <VariableCollection />
	            <ProcessDataCollection />
	            <EventCollection />
            </DeviceFunction>";
        
        private DeviceFunction Function { get; set; }
        
        [TestInitialize]
        public void Initialize()
        {
            Function = new DeviceFunction();
            Function.Deserialize(XElement.Parse(XmlText));
        }
        
        [TestMethod]
        public void HasVariableCollection()
        {
            Assert.IsNotNull(Function.VariableCollection);
        }
        
        [TestMethod]
        public void HasProcessDataCollection()
        {
            Assert.IsNotNull(Function.ProcessDataCollection);
        }
        
        [TestMethod]
        public void HasEventCollection()
        {
            Assert.IsNotNull(Function.EventCollection);
        }

        [TestMethod]
        public void IsCorrectAml()
        {
            var amlCollection = Function.ToAml();
            Assert.IsNotNull(amlCollection);
        }

    }

}