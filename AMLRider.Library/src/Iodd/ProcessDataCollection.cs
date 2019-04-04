using System.Collections;
using System.Collections.Generic;

namespace AMLRider.Library.Iodd
{
    
    public class ProcessDataCollection : IEnumerable<ProcessData>
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
    }
    
}