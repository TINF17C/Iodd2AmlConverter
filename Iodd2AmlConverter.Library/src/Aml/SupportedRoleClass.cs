using System.Xml.Linq;

namespace Iodd2AmlConverter.Library.Aml
{

    public class SupportedRoleClass : AmlElement
    {

        private const string DefaultRefRoleClassPath = "AutomationMLComponentBaseRCL/AdditionalDeviceDescription";
        
        public string RefRoleClassPath { get; set; }


        public override XElement Serialize()
        {
            var element = new XElement("SupportedRoleClass");
            element.SetAttributeValue("RefRoleClassPath", RefRoleClassPath ?? DefaultRefRoleClassPath);

            return element;
        }

    }

}