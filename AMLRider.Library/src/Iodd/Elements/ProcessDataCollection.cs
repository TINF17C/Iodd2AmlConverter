using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{
    
    public class ProcessDataCollection : IoddElement, IEnumerable<ProcessData>
    {
        
        private List<ProcessData> ProcessDataList { get; }

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

        public override AmlElement ToAml()
        {
            var element = new InternalElement
            {
                Name = "ProcessDataCollection",
                Id = "ProcessDataCollection"
            };

            foreach (var processData in ProcessDataList)
            {
                var amlElement = processData.ToAml() as InternalElement;
                element.InternalElements.Add(amlElement);
            }

            return element;
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