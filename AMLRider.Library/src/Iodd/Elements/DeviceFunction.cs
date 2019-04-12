using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{
    public class DeviceFunction : IoddElement
    {
        public Features Features { get; set; }

        [Optional]
        public DataTypeCollection DataTypes { get; set; }

        public VariableCollection Variables { get; set; }

        public override void Deserialize(XElement element)
        {
            if (element.SubElement("Features") != null)
            {
                Features = new Features();
                Features.Deserialize(element.SubElement("Features"));
            }

            Variables = new VariableCollection();
            Variables.Deserialize(element.SubElement("VariableCollection"));

            if (element.SubElement("DatatypeCollection") == null)
                return;

            DataTypes = new DataTypeCollection();
            DataTypes.Deserialize(element.SubElement("DatatypeCollection"));
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
    }
}