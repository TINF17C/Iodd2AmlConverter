using System;
using System.ComponentModel;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    
    public class Iso15745Reference : IoddElement
    {

        [DefaultValue("1")]
        public string Iso15745Part { get; set; } = "1";

        [DefaultValue("1")]
        public string Iso15745Edition { get; set; } = "1";

        [DefaultValue("IODD")]
        public string ProfileTechnology { get; set; } = "IODD";


        public override void Deserialize(XElement element)
        {
            // Do nothing
        }

        public override AmlCollection ToAml()
        {
            throw new NotImplementedException();
        }
    }
    
}