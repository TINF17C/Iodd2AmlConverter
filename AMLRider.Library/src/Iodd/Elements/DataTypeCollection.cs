using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;
using AMLRider.Library.Iodd.DataTypes;

namespace AMLRider.Library.Iodd.Elements
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

        public override AmlElement ToAml()
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

            return element;
        }

    }

}