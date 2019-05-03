using System.Xml.Linq;

namespace Iodd2AmlConverter.Library.Aml
{
    public abstract class AmlElement : IAmlSerializable
    {
        
        public abstract XElement Serialize();
        
    }
}