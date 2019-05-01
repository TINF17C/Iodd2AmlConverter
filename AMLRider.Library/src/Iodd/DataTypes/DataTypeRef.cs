using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.DataTypes
{

    public class DataTypeRef
    {

        #region Attributes

        public string DataTypeId { get; set; }

        #endregion

        public void Deserialize(XElement element)
        {
            DataTypeId = element.GetAttributeValue("datatypeId");
        }

        public AmlElement ToAml()
        {
            var attribute = new Attribute
            {
                Name = DataTypeId,
                RefSemantic = new RefSemantic
                {
                    CorrespondingAttributePath = "ListType"
                }
            };

            return attribute;
        }

    }

}