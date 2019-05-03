using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;

namespace Iodd2AmlConverter.Library.Iodd
{
    public abstract class IoddElement : IDeserializableIodd, IAmlConvertible
    {
        
        public abstract void Deserialize(XElement element);

        public abstract AmlElement ToAml();
        
    }
}