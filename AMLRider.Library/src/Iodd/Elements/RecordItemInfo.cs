using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{
    
    public class RecordItemInfo : IoddElement
    {
        
        #region Attributes
        
        public int SubIndex { get; set; }
        
        [Optional]
        public string DefaultValue { get; set; }
        
        [Optional]
        public bool ModifiesOtherVariables { get; set; }
        
        [Optional]
        public bool ExcludedFromDataStorage { get; set; }

        #endregion
        
        public override void Deserialize(XElement element)
        {
            SubIndex = int.Parse(element.GetAttributeValue("subindex"));

            if (element.HasAttribute("defaultValue"))
                DefaultValue = element.GetAttributeValue("defaultValue");

            if (element.HasAttribute("modifiesOtherVariables"))
                ModifiesOtherVariables = bool.Parse(element.GetAttributeValue("modifiesOtherVariables"));

            if (element.HasAttribute("excludeFromDataStorage"))
                ExcludedFromDataStorage = bool.Parse(element.GetAttributeValue("excludedFromDataStorage"));
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
    }
    
}