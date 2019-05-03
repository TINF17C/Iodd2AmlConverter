using System.Xml.Linq;

namespace Iodd2AmlConverter.Library.Aml
{
    public interface IAmlSerializable
    {

        XElement Serialize();

    }
}