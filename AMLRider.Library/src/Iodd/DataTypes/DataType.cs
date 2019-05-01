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
            Id = element.GetAttributeValue("type");
        }
        
        public virtual AmlElement ToAml()
        {
            return new Attribute
            {
                Name = Id
            };
        }

    }
    
}