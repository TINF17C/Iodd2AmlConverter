using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
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
            throw new System.NotImplementedException();
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
        
    }
}