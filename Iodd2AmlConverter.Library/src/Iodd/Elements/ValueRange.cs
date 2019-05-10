using System;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Extensions;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    
    public class ValueRange : IoddElement
    {
        
        #region Attributes
        
        public int LowerValue { get; set; }
        
        public int UpperValue { get; set; }
        
        #endregion
        
        [Optional]
        public Name Name { get; set; }

        public override void Deserialize(XElement element)
        {
            LowerValue = int.Parse(element.GetAttributeValue("lowerValue"));
            UpperValue = int.Parse(element.GetAttributeValue("upperValue"));

            if (element.SubElement("Name") == null)
                return;

            Name = new Name();
            Name.Deserialize(element.SubElement("Name"));
        }

        public override AmlCollection ToAml()
        {
            return AmlCollection.Emtpy();
        }
    }
    
}