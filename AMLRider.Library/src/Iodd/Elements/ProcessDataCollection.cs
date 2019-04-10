using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;

namespace AMLRider.Library.Iodd.Elements
{
    
    public class ProcessDataCollection : IoddElement, IEnumerable<ProcessData>
    {
        
        private List<ProcessData> ProcessDataList { get; }

        public ProcessDataCollection()
        {
            ProcessDataList = new List<ProcessData>();
        }

        public void Add(ProcessData processData)
        {
            ProcessDataList.Add(processData);
        }

        public ProcessData Get(int index)
        {
            return ProcessDataList[index];
        }

        public IEnumerator<ProcessData> GetEnumerator()
        {
            return ProcessDataList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

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