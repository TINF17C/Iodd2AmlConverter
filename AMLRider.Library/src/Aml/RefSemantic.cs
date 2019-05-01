using System.Xml.Linq;

namespace AMLRider.Library.Aml
{

    public class RefSemantic : AmlElement
    {

        public string CorrespondingAttributePath { get; set; }

        public override XElement Serialize()
        {
            var element = new XElement("RefSemantic");
            if(CorrespondingAttributePath != null)
                element.SetAttributeValue("CorrespondingAttributePath", CorrespondingAttributePath);

            return element;
        }

    }

}