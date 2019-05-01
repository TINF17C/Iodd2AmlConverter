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

        [Optional]
        public ProcessDataIn ProcessDataIn { get; set; }
        
        /// <summary>
        /// This is optional.
        /// </summary>
        [Optional]
        public Condition Condition { get; set; }

        #endregion

        public override void Deserialize(XElement element)
        {
            Id = element.GetAttributeValue("id");

            if (element.SubElement("ProcessDataIn") != null)
            {
                ProcessDataIn = new ProcessDataIn();
                ProcessDataIn.Deserialize(element.SubElement("ProcessDataIn"));
            }
            
            if (element.SubElement("Condition") == null) 
                return;
            
            Condition = new Condition();
            Condition.Deserialize(element.SubElement("Condition"));
        }

        public override AmlElement ToAml()
        {
            var element = new InternalElement
            {
                Name = Id
            };

            if (ProcessDataIn != null)
            {
                var amlElement = ProcessDataIn.ToAml() as InternalElement;
                element.InternalElements.Add(amlElement);
            }

            if (Condition != null)
            {
                var amlElement = Condition.ToAml() as InternalElement;
                element.InternalElements.Add(amlElement);
            }

            return element;
        }
    }
}