using System.Xml.Linq;
using AMLRider.Library.Aml;

namespace AMLRider.Library.Iodd.Elements
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