using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{

    public class Test : IoddElement
    {

        #region Elements
        
        [Optional]
        public List<Config> Configs { get; set; }
        
        [Optional]
        public Config7 Config7 { get; set; }
        
        #endregion
        
        public override void Deserialize(XElement element)
        {
            foreach (var subElement in element.Elements().Where(x => x.Name.LocalName != "Config7"))
            {
                var config = new Config();
                config.Deserialize(subElement);
                
                Configs.Add(config);
            }

            if (element.SubElement("Config7") != null)
            {
                Config7 = new Config7();
                Config7.Deserialize(element.SubElement("Config7"));
            }
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }

    }

}