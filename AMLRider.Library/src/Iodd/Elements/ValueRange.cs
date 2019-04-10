using System.Xml.Linq;
using AMLRider.Library.Aml;

namespace AMLRider.Library.Iodd.Elements
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
            throw new System.NotImplementedException();
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
    }
    
}