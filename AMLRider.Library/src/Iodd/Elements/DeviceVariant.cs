using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{
    public class DeviceVariant : IoddElement
    {

        #region Attributes
        
        /// <summary>
        /// The product ID.
        /// </summary>
        public int ProductId { get; set; }
        
        [Optional]
        public string DeviceSymbol { get; set; }
        
        [Optional]
        public string DeviceIcon { get; set; }

        #endregion

        #region Elements

        public Name Name { get; set; }
        
        public Description Description { get; set; }

        #endregion

        public DeviceVariant()
        {
            Name = new Name();
            Description = new Description();
        }

        public override void Deserialize(XElement element)
        {
            ProductId = int.Parse(element.GetAttributeValue("productId"));
            DeviceSymbol = element.GetAttributeValue("deviceSymbol");
            DeviceIcon = element.GetAttributeValue("deviceIcon");
            
            Name.Deserialize(element.Element("Name"));            
            Description.Deserialize(element.Element("Description"));
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
    }
}