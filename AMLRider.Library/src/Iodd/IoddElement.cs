using System.Xml.Linq;
using AMLRider.Library.Aml;

namespace AMLRider.Library.Iodd
{
    public abstract class IoddElement : IDeserializableIodd, IAmlConvertible
    {
        
        public abstract void Deserialize(XElement element);

        public abstract AmlElement ToAml();
        
    }
}