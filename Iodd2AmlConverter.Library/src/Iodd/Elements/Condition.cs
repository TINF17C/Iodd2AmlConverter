using System;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    
    /// <summary>
    /// Serves to switch between different process data.
    /// </summary>
    public class Condition : IoddElement
    {
        
        /// <summary>
        /// References a variable.
        /// </summary>
        public string VariableId { get; set; }
        
        /// <summary>
        /// Addresses the record item within the record.
        /// </summary>
        [Optional]
        public byte? SubIndex { get; set; }
        
        /// <summary>
        /// The value of a variable.
        /// </summary>
        public byte Value { get; set; }

        public override void Deserialize(XElement element)
        {
            VariableId = element.GetAttributeValue("variableId");
            
            if (element.HasAttribute("subindex"))
                SubIndex = byte.Parse(element.GetAttributeValue("subindex"));

            Value = byte.Parse(element.GetAttributeValue("value"));
        }

        public override AmlCollection ToAml()
        {
            throw new NotImplementedException();
        }
        
    }
}