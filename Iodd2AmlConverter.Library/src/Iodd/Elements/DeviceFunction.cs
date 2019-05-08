using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;
using Iodd2AmlConverter.Library.Extensions;

namespace Iodd2AmlConverter.Library.Iodd.Elements
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

        public override AmlCollection ToAml()
        {
            var element = new InternalElement
            {
                Name = "DeviceFunction",
                Id = "DeviceFunction"
            };

            if (Features != null)
                element.InternalElements.Add(Features.ToAml() as InternalElement);

            if (VariableCollection != null)
                element.InternalElements.Add(VariableCollection.ToAml() as InternalElement);

            if (DataTypeCollection != null)
                element.InternalElements.AddRange(DataTypeCollection.ToAml().Cast<InternalElement>());

            if (ProcessDataCollection != null)
                element.InternalElements.Add(ProcessDataCollection.ToAml() as InternalElement);

            return AmlCollection.Of(element);
        }

    }

}