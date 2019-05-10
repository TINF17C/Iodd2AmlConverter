using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;
using Iodd2AmlConverter.Library.Extensions;

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

        public override AmlCollection ToAml()
        {
            var element = new InternalElement
            {
                Name = Id
            };

            foreach (var processDataIn in ProcessDataIns)
            {
                var amlElement = processDataIn.ToAml().Cast<InternalElement>();
                element.InternalElements.AddRange(amlElement);
            }

            if (Condition != null)
            {
                var amlElement = Condition.ToAml().Cast<InternalElement>();
                element.InternalElements.AddRange(amlElement);
            }

            return AmlCollection.Of(element);
        }

    }

}