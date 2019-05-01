using System;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.DataTypes
{
    public class ComplexDataType : DataType
    {
        public int Id { get; set; }
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