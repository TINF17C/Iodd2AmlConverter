using System.ComponentModel;
using System.Xml.Linq;
using AMLRider.Library.Aml;

namespace AMLRider.Library.Iodd.Elements
{
    
    /// <summary>
    /// Provider for constant vendor information.
    /// </summary>
    public class ProfileHeader : IoddElement
    {
        [DefaultValue("IO Device Profile")]
        public string ProfileIdentification { get; set; } = "IO Device Profile";

        [DefaultValue("1.1")]
        public string ProfileRevision { get; set; } = "1.1";

        [DefaultValue("Device Profile for IO Devices")]
        public string ProfileName { get; set; } = "Device Profile for IO Devices";

        [DefaultValue("IO-Link Consortium")]
        public string ProfileSource { get; set; } = "IO-Link Consortium";

        [DefaultValue("Device")]
        public string ProfileClassId { get; set; } = "Device";
        
        public Iso15745Reference Iso15745Reference { get; set; } = new Iso15745Reference();

        public override void Deserialize(XElement element)
        {
            // Do nothing
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
        
    }
}