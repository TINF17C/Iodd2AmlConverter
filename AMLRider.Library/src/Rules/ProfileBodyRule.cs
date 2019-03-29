using System.Xml.Linq;
using AMLRider.Library.Rules;
using System;
using System.Collections.Generic;
using System.Text;
using AMLRider.Library.Helpers;
using AMLRider.Library.Extensions;


namespace AMLRider.Library.Rules
{
    public class ProfileBodyRule : IConversionRule
    {
        public bool CanApplyRule(XElement element)
        {
            return element.Name.LocalName == "ProfileBody";
        }

        public XElement Apply(XElement element)
        {
            if (!CanApplyRule(element))
                throw new InvalidOperationException("The given node is not a <ProfileBody> node.");

            var valueTag = element.Value;
            return CreateProfileBodyTag(valueTag);
        }

        private static XElement CreateProfileBodyTag(string valueTag)
        {
            var systemUnitClassLib = XmlHelper.CreateElement("SystemUnitClassLib");
            systemUnitClassLib.SetAttributeValue("Name", "ComponentSystemUnitClassLib");

            var systemUnitClass = XmlHelper.CreateElement(systemUnitClassLib, "SystemUnitClass");
            systemUnitClass.SetAttributeValue("Name", "Device");
            systemUnitClass.SetAttributeValue("Name", Guid.NewGuid().ToString());
            systemUnitClass.Value = valueTag;

            return systemUnitClassLib;
        }
    }
}