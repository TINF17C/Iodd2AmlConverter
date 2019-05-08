using System.Collections;
using System.Collections.Generic;

namespace Iodd2AmlConverter.Library.Aml
{

    public class AmlCollection : IEnumerable<AmlElement>
    {

        private List<AmlElement> AmlElements { get; }

        public AmlCollection()
        {
            AmlElements = new List<AmlElement>();
        }

        public void Add(AmlElement element)
        {
            AmlElements.Add(element);
        }

        public IEnumerator<AmlElement> GetEnumerator()
        {
            return AmlElements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static AmlCollection Of(params AmlElement[] elements)
        {
            var collection = new AmlCollection();
            foreach (var element in elements)
                collection.Add(element);

            return collection;
        }

    }

}