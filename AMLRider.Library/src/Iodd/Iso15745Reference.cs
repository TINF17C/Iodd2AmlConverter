using System.ComponentModel;

namespace AMLRider.Library.Iodd
{
    
    public class Iso15745Reference
    {

        [DefaultValue("1")]
        public string Iso15745Part { get; set; } = "1";

        [DefaultValue("1")]
        public string Iso15745Edition { get; set; } = "1";

        [DefaultValue("IODD")]
        public string ProfileTechnology { get; set; } = "IODD";

    }
    
}