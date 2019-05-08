using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;

namespace Iodd2AmlConverter.Library.Iodd.DataTypes
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