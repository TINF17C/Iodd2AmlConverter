using System.Xml.Linq;
using AMLRider.Library.Aml;

namespace AMLRider.Library.Iodd.Elements
{
    
    public class SingleValue : IoddElement
    {
        
        #region Attributes
        
        public string Value { get; set; }
        
        #endregion
        
        #region Elements
        
        [Optional]
        public Name Name { get; set; }
        
        #endregion

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