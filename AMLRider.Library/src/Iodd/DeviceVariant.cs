using System.Xml.Linq;
using AMLRider.Library.Extensions;
using AMLRider.Library.Utils;

namespace AMLRider.Library.Iodd
{
    public class DeviceVariant : IIoddElement
    {

        #region Attributes
        
        /// <summary>
        /// The product ID.
        /// </summary>
        public int ProductId { get; set; }
        
        public Optional<string> DeviceSymbol { get; set; }
        
        public Optional<string> DeviceIcon { get; set; }

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

        public void Deserialize(XElement element)
        {
            ProductId = int.Parse(element.GetAttributeValue("productId"));
            DeviceSymbol = Optional<string>.OfNullable(element.GetAttributeValue("deviceSymbol"));
            DeviceIcon = Optional<string>.OfNullable(element.GetAttributeValue("deviceIcon"));
            
            Name.Deserialize(element.Element("Name"));            
            Description.Deserialize(element.Element("Description"));
        }
        
    }
}