using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Extensions;

namespace Iodd2AmlConverter.Library.Iodd.Elements
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

            if (element.SubElement("Config7") == null) 
                return;
            
            Config7 = new Config7();
            Config7.Deserialize(element.SubElement("Config7"));
        }

        public override AmlCollection ToAml()
        {
            return AmlCollection.Emtpy();
        }

    }

}