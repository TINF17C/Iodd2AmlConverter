using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;
using Microsoft.Win32.SafeHandles;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{

    public class EventCollection : IoddElement
    {

        public List<StdEventRef> StdEventRefs { get; set; }
        public List<Event> Events { get; set; }

        public override void Deserialize(XElement element)
        {
            var stdEventRefs = element.SubElements("StdEventRef");
            var events = element.SubElements("Events");

            foreach (var stdEventRef in stdEventRefs)
            {
                var stdEventRefVar = new StdEventRef();
                stdEventRefVar.Deserialize(stdEventRef);
                StdEventRefs.Add(stdEventRefVar);
            }

            foreach (var @event in events)
            {
                var eventVar = new Event();
                eventVar.Deserialize(@event);
                Events.Add(eventVar);
            }
        }

        public override AmlCollection ToAml()
        {
            var internalElement = new InternalElement
            {
                Name = "EventCollection",
                Id = "EventCollection"
            };

            foreach (var @event in Events)
            {
                internalElement.InternalElements.AddRange(@event.ToAml().Cast<InternalElement>());
            }

            foreach (var stdEventRef in StdEventRefs)
            {
                internalElement.InternalElements.AddRange(stdEventRef.ToAml().Cast<InternalElement>());
            }

            return AmlCollection.Of(internalElement);
        }

    }

}