using System.Xml.Linq;
using AMLRider.Library.Aml;

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
            throw new System.NotImplementedException();
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
        
    }
}