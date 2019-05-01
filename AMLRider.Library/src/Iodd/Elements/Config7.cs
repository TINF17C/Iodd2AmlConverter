using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{

    public class Config7 : IoddElement
    {

        #region Attributes

        public int Index { get; set; }

        #endregion

        #region Elements

        public List<EventTrigger> EventTriggers { get; set; }

        #endregion

        public Config7()
        {
            EventTriggers = new List<EventTrigger>();
        }

        public override void Deserialize(XElement element)
        {
            foreach (var subElement in element.SubElements("EventTrigger"))
            {
                var trigger = new EventTrigger();
                trigger.Deserialize(subElement);
                
                EventTriggers.Add(trigger);
            }
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }

    }

}