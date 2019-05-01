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
                var dataType = CreateDataTypeBasedOnId(id, subElement);

                Add(dataType);
            }
        }

        private DataType CreateDataTypeBasedOnId(string id, XElement element)
        {
            switch (id)
            {
                case "BooleanT":
                {
                    var dataType = new BooleanT();
                    dataType.Deserialize(element);

                    return dataType;
                }

                case "Float32T":
                {
                    var dataType = new Float32T();
                    dataType.Deserialize(element);

                    return dataType;
                }

                case "OctetStringT":
                {
                    var dataType = new OctetStringT();
                    dataType.Deserialize(element);

                    return dataType;
                }

                case "StringT":
                {
                    var dataType = new StringT();
                    dataType.Deserialize(element);

                    return dataType;
                }

                case "TimeSpanT":
                {
                    var dataType = new TimeSpanT();
                    dataType.Deserialize(element);

                    return dataType;
                }

                case "TimeT":
                {
                    var dataType = new TimeT();
                    dataType.Deserialize(element);

                    return dataType;
                }

                case "UIntegerT":
                {
                    var dataType = new UIntegerT();
                    dataType.Deserialize(element);

                    return dataType;
                }
                
                default:
                {
                    var dataType = new DataType();
                    dataType.Deserialize(element);

                    return dataType;
                }
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