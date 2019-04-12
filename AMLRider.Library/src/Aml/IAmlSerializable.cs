using System.Xml.Linq;

namespace AMLRider.Library.Aml
{
    public interface IAmlSerializable
    {

        XElement Serialize();

    }
}