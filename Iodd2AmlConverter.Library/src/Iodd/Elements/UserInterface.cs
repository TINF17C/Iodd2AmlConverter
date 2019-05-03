using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    public class UserInterface : IoddElement
    {
        public override void Deserialize(XElement element)
        {
            
        }

        public override AmlElement ToAml()
        {
            var internalElement = new InternalElement();
            internalElement.Name = "UserInterface";
            internalElement.Id = "UserInterface";

            return internalElement;
        }
    }
}