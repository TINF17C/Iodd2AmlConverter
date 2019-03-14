using System;
using System.Xml.Linq;

namespace AMLRider.Library.Rules
{
    /// <inheritdoc />
    /// <summary>
    /// 
    /// </summary>
    public class DeviceIdentityRule : IConversionRule
    {
        
        public bool CanApplyRule(XElement element)
        {
            return element.Name == "DeviceIdentity";
        }

        public XElement Apply(XElement element)
        {
            if(!CanApplyRule(element))
                throw new InvalidOperationException("The given node is not a <DeviceIdentity> node.");
            
            var deviceIdentityObj = GetDeviceIdentity(element);
            return CreateDeviceIdentityTag(deviceIdentityObj);
        }
        
        private static GetDeviceIdentity()
    }
}