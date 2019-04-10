using System.Xml.Linq;

namespace AMLRider.Library.Iodd
{
    
    public interface IDeserializableIodd
    {
    
        void Deserialize(XElement element);

    }
    
}