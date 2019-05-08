using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{

    public class Config : IoddElement
    {

        public int Index { get; set; }
        
        public string TestValue { get; set; }
        
        public override void Deserialize(XElement element)
        {
            Index = int.Parse(element.GetAttributeValue("index"));
            TestValue = element.GetAttributeValue("testValue");
        }

        public override AmlCollection ToAml()
        {
            throw new System.NotImplementedException();
        }

    }

}