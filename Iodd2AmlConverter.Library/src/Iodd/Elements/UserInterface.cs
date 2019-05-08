using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    public class UserInterface : IoddElement
    {
        public override void Deserialize(XElement element)
        {
            
        }

        public override AmlCollection ToAml()
        {
            var internalElement = new InternalElement();
            internalElement.Name = "UserInterface";
            internalElement.Id = "UserInterface";

            return AmlCollection.Of(internalElement);
        }
    }
}