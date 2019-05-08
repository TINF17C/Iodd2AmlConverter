using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    
    public class ProcessDataCollection : IoddElement, IEnumerable<ProcessData>
    {
        
        public List<ProcessData> ProcessDataList { get; }

        public ProcessDataCollection()
        {
            ProcessDataList = new List<ProcessData>();
        }
        
        public override void Deserialize(XElement element)
        {
            foreach (var processDataElement in element.SubElements("ProcessData"))
            {
                var processData = new ProcessData();
                processData.Deserialize(processDataElement);
                
                ProcessDataList.Add(processData);
            }
        }

        public override AmlCollection ToAml()
        {
            var element = new InternalElement
            {
                Name = "ProcessDataCollection",
                Id = "ProcessDataCollection"
            };

            foreach (var processData in ProcessDataList)
            {
                var amlElement = processData.ToAml().Cast<InternalElement>();
                element.InternalElements.AddRange(amlElement);
            }

            return AmlCollection.Of(element);
        }

        public IEnumerator<ProcessData> GetEnumerator()
        {
            return ProcessDataList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
}