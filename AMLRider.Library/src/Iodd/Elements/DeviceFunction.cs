using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{
    public class DeviceFunction : IoddElement
    {
        public Features Features { get; set; }

        [Optional]
        public DataTypeCollection DataTypeCollection { get; set; }

        public VariableCollection VariableCollection { get; set; }
        
        public ProcessDataCollection ProcessDataCollection { get; set; }

        public override void Deserialize(XElement element)
        {
            if (element.SubElement("Features") != null)
            {
                Features = new Features();
                Features.Deserialize(element.SubElement("Features"));
            }

            VariableCollection = new VariableCollection();
            VariableCollection.Deserialize(element.SubElement("VariableCollection"));
            
            ProcessDataCollection = new ProcessDataCollection();
            ProcessDataCollection.Deserialize(element.SubElement("ProcessDataCollection"));
            
            if (element.SubElement("DatatypeCollection") == null)
                return;

            DataTypeCollection = new DataTypeCollection();
            DataTypeCollection.Deserialize(element.SubElement("DatatypeCollection"));
        }

        public override AmlElement ToAml()
        {
            var element = new InternalElement
            {
                Name = "DeviceFunction",
                Id = "DeviceFunction"
            };
            
            if(Features != null)
                element.InternalElements.Add(Features.ToAml() as InternalElement);
            
            element.InternalElements.Add(VariableCollection.ToAml() as InternalElement);
            element.InternalElements.Add(DataTypeCollection.ToAml() as InternalElement);
            element.InternalElements.Add(ProcessDataCollection.ToAml() as InternalElement);

            return element;
        }
    }
}