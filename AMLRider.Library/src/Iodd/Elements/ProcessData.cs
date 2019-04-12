using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{
    public class ProcessData : IoddElement
    {
        
        #region Attributes

        public string Id { get; set; }

        #endregion

        #region Elements

        /// <summary>
        /// This is optional.
        /// </summary>
        [Optional]
        public Condition Condition { get; set; }

        #endregion

        public override void Deserialize(XElement element)
        {
            Id = element.GetAttributeValue("id");

            if (element.SubElement("Condition") == null) 
                return;
            
            Condition = new Condition();
            Condition.Deserialize(element.SubElement("Condition"));
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
    }
}