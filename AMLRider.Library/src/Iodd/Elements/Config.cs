using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
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

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }

    }

}