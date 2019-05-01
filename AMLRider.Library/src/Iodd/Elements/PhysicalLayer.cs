using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{
    public class PhysicalLayer : IoddElement
    {
        public string BitRate { get; set; }
        public int MinCycleTime { get; set; }
        public bool SioSupported { get; set; }
        public byte MSequenceCapability { get; set; }
        //public List<Connection> Connections { get; set; }
        
        public override void Deserialize(XElement element)
        {
            BitRate = element.GetAttributeValue("bitrate");
            MinCycleTime = int.Parse(element.GetAttributeValue("minCycleTime"));
            SioSupported = bool.Parse(element.GetAttributeValue("sioSupported"));
            MSequenceCapability = byte.Parse(element.GetAttributeValue("mSequenceCapability"));
            foreach (var connection in element.SubElements("Connection"))
            {
                //var connectionVar = new Connection();
                //connectionVar.Deserialize(connection);
                //Connections.Add(connectionVar);
            }
        }

        public override AmlElement ToAml()
        {
            throw new NotImplementedException();
        }
    }
}