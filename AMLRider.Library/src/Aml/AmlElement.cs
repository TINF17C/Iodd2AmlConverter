using System.Xml.Linq;

namespace AMLRider.Library.Aml
{
    public abstract class AmlElement : IAmlSerializable
    {
        
        public abstract XElement Serialize();
        
    }
}