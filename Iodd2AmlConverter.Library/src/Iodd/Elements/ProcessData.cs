using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{

    public class ProcessData : IoddElement
    {

        #region Attributes

        public string Id { get; set; }

        #endregion

        #region Elements

        [Optional]
        public List<ProcessDataIn> ProcessDataIns { get; set; }

        /// <summary>
        /// This is optional.
        /// </summary>
        [Optional]
        public Condition Condition { get; set; }

        #endregion

        public ProcessData()
        {
            ProcessDataIns = new List<ProcessDataIn>();
        }

        public override void Deserialize(XElement element)
        {
            Id = element.GetAttributeValue("id");

            foreach (var processDataElement in element.SubElements("ProcessDataIn")
                .Concat(element.SubElements("ProcessDataOut")))
            {
                var processDataIn = new ProcessDataIn();
                processDataIn.Deserialize(processDataElement);

                ProcessDataIns.Add(processDataIn);
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

            foreach (var processDataIn in ProcessDataIns)
            {
                var amlElement = processDataIn.ToAml() as InternalElement;
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