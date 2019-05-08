using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;
using Iodd2AmlConverter.Library.Iodd.DataTypes;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{

    public class DataTypeCollection : IoddElement, IEnumerable<DataType>
    {

        private List<DataType> DataTypes { get; }

        public DataTypeCollection()
        {
            DataTypes = new List<DataType>();
        }

        public void Add(DataType dataType)
        {
            DataTypes.Add(dataType);
        }

        public DataType Get(int index)
        {
            return DataTypes[index];
        }

        public IEnumerator<DataType> GetEnumerator()
        {
            return DataTypes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override void Deserialize(XElement element)
        {
            foreach (var subElement in element.SubElements("Datatype"))
            {
                var id = subElement.GetAttributeValue("type");
                var dataType = DataType.CreateDataTypeBasedOnId(id, subElement);

                Add(dataType);
            }
        }

        public override AmlCollection ToAml()
        {
            var element = new InternalElement
            {
                Name = "DatatypeCollection",
                Id = "DatatypeCollection"
            };

            foreach (var dataType in DataTypes)
            {
                var amlElement = dataType.ToAml() as Attribute;
                element.Attributes.Add(amlElement);
            }

            return AmlCollection.Of(element);
        }

    }

}