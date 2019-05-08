using System;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;
using Attribute = Iodd2AmlConverter.Library.Aml.Elements.Attribute;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    
    public class SupportedAccessLocks : IoddElement
    {
        
        /// <summary>
        /// Specifies whether parameter access lock is supported.
        /// </summary>
        public bool Parameter { get; set; }
        
        /// <summary>
        /// Specifies whether data storage access lock is supported.
        /// </summary>
        public bool DataStorage { get; set; }
        
        /// <summary>
        /// Specifies whether local parameterization access lock is supported.
        /// </summary>
        public bool LocalParameterization { get; set; }
        
        /// <summary>
        /// Specifies whether local user interface access lock is supported.
        /// </summary>
        public bool LocalUserInterface { get; set; }

        public override void Deserialize(XElement element)
        {
            Parameter = bool.Parse(element.GetAttributeValue("parameter"));
            DataStorage = bool.Parse(element.GetAttributeValue("dataStorage"));
            LocalParameterization = bool.Parse(element.GetAttributeValue("localParameterization"));
            LocalUserInterface = bool.Parse(element.GetAttributeValue("localUserInterface"));
        }

        public override AmlCollection ToAml()
        {
            var element = new InternalElement
            {
                Name = "SupportedAccessLocks"
            };
            
            element.Attributes.Add(CreateAttribute("parameter", Parameter));
            element.Attributes.Add(CreateAttribute("dataStorage", DataStorage));
            element.Attributes.Add(CreateAttribute("localUserInterface", LocalUserInterface));
            element.Attributes.Add(CreateAttribute("localParameterization", LocalParameterization));

            return AmlCollection.Of(element);
        }
        
        private static Attribute CreateAttribute(string name, bool value)
        {
            return new Attribute
            {
                Name = name,
                AttributeDataType = "xs:boolean",
                Value = new Value
                {
                    Content = value.ToString()
                }
            };
        }
        
    }
    
}