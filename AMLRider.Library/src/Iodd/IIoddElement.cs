using System.Xml.Linq;

namespace AMLRider.Library.Iodd
{
    
    public interface IIoddElement
    {
    
        void Deserialize(XElement element);

    }
    
}