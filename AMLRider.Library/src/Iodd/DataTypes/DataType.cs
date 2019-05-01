using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.DataTypes
{
    
    /// <summary>
    /// Definition of a data type.
    /// </summary>
    public class DataType : IDeserializableIodd
    {
        
        #region Attributes
        
        /// <summary>
        /// The data type ID.
        /// </summary>
        [Optional]
        public string Id { get; set; }
        
        #endregion

        public virtual void Deserialize(XElement element)
        {
            Id = element.Attribute(element.GetNamespaceOfPrefix("xsi") + "type")?.Value;
        }
        
        public virtual AmlElement ToAml()
        {
            return new Attribute
            {
                Name = Id
            };
        }
        
        public static DataType CreateDataTypeBasedOnId(string id, XElement element)
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

                case "IntegerT":
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

    }
    
}