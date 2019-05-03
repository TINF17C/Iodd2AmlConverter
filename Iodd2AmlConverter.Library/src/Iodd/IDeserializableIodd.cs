using System.Xml.Linq;

namespace Iodd2AmlConverter.Library.Iodd
{
    
    public interface IDeserializableIodd
    {
    
        void Deserialize(XElement element);

    }
    
}