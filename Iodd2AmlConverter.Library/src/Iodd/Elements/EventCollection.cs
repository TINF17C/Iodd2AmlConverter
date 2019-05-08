using System.Collections.Generic;
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

        public override AmlElement ToAml()
        {
            var internalElement = new InternalElement();
            internalElement.Name = "EventCollection";
            internalElement.Id = "EventCollection";

            foreach (var @event in Events)
            {
                internalElement.InternalElements.Add(@event.ToAml() as InternalElement);
            }

            foreach (var stdEventRef in StdEventRefs)
            {
                internalElement.InternalElements.Add(stdEventRef.ToAml() as InternalElement);
            }

            return internalElement;
        }
    }
}