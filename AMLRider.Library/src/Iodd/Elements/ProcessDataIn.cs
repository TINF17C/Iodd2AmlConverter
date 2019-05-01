using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;
using AMLRider.Library.Iodd.DataTypes;

namespace AMLRider.Library.Iodd.Elements
{

    public class ProcessDataIn : IoddElement
    {

        #region Attributes

        public string Id { get; set; }

        public int BitLength { get; set; }

        #endregion

        #region Elements

        public List<DataType> DataTypes { get; set; }

        public List<DataTypeRef> DataTypeRefs { get; set; }

        public Name Name { get; set; }

        #endregion

        public ProcessDataIn()
        {
            DataTypes = new List<DataType>();
            DataTypeRefs = new List<DataTypeRef>();
        }

        public override void Deserialize(XElement element)
        {
            Id = element.GetAttributeValue("id");
            BitLength = int.Parse(element.GetAttributeValue("bitLength"));

            foreach (var subElement in element.SubElements("Datatype"))
            {
                // TODO:
                var dataType = new DataType();
                dataType.Deserialize(subElement);
                
                DataTypes.Add(dataType);
            }

            foreach (var subElement in element.SubElements("DatatypeRef"))
            {
                var dataTypeRef = new DataTypeRef();
                dataTypeRef.Deserialize(subElement);
                
                DataTypeRefs.Add(dataTypeRef);
            }

            if (element.SubElement("Name") != null)
            {
                Name = new Name();
                Name.Deserialize(element.SubElement("Name"));
            }
        }

        public override AmlElement ToAml()
        {
            var element = new InternalElement
            {
                Name = Id
            };

            element.Attributes.Add(new Attribute
            {
                Name = "bitLength",
                AttributeDataType = "xs:integer",
                Value = new Value
                {
                    Content = BitLength.ToString()
                }
            });

            foreach (var dataType in DataTypes)
            {
                var amlElement = dataType.ToAml();
                element.Attributes.Add(amlElement as Attribute);
            }

            foreach (var dataTypeRef in DataTypeRefs)
            {
                var amlElement = dataTypeRef.ToAml();
                element.Attributes.Add(amlElement as Attribute);
            }
            
            return element;
        }

    }

}