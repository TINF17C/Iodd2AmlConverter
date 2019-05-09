using System;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;
using Iodd2AmlConverter.Library.Extensions;

namespace Iodd2AmlConverter.Library.Iodd.DataTypes
{
    public class ComplexDataType : DataType
    {
        public new int Id { get; set; }
        
        public bool SubindexAccessSupported { get; set; }

        public override void Deserialize(XElement element)
        {
            Id = int.Parse(element.GetAttributeValue("id"));
            SubindexAccessSupported = bool.Parse(element.GetAttributeValue("subindexAccessSupported"));
        }

        public override AmlElement ToAml()
        {
            // TODO
            var internalElement = new InternalElement();

            return internalElement;
        }
    }
}