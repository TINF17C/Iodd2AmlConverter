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
            Features = new Features();
            Features.Deserialize(element.Element("Features"));
            
            Variables = new VariableCollection();
            Variables.Deserialize(element.Element("VariableCollection"));

            if (element.Element("DatatypeCollection") == null) 
                return;
            
            DataTypes = new DataTypeCollection();
            DataTypes.Deserialize(element.Element("DatatypeCollection"));
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
        
    }
}