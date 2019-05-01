using AMLRider.Library.Aml;

namespace AMLRider.Library.Iodd.DataTypes
{
    public class StringT : SimpleDataType
    {

        #region Attributes

        public int FixedLength { get; set; }
        
        public string Encoding { get; set; }

        #endregion

        public override AmlElement ToAml()
        {
            return new Attribute
            {
                Name = "fixedLength",
                AttributeDataType = "xs:integer",
                Value = new Value
                {
                    Content = FixedLength.ToString()
                }
            };
        }

    }
}