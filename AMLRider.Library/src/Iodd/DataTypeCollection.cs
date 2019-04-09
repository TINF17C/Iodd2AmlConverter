using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Iodd.DataTypes;

namespace AMLRider.Library.Iodd
{
    
    public class DataTypeCollection : IEnumerable<DataType>, IIoddElement
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

        public void Deserialize(XElement element)
        {
            throw new System.NotImplementedException();
        }
    }
}