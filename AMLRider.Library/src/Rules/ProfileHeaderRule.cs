using System;
using System.Xml.Linq;
using AMLRider.Library.Helpers;
using AMLRider.Library.Rules.DataObjects;

namespace AMLRider.Library.Rules
{
    
    /// <inheritdoc />
    /// <summary>
    /// Responsible for converting the ProfileHeader element.
    /// </summary>
    public class ProfileHeaderRule : IConversionRule
    {
        
        /// <summary>
        /// Extracts the information of the ProfileHeader element.
        /// </summary>
        /// <param name="root">The ProfileHeader root element.</param>
        /// <returns>The extracted ProfileHeader information.</returns>
        private static ProfileHeaderObj ExtractHeaderInformation(XContainer root)
        {
            var headerObj = new ProfileHeaderObj
            {
                ProfileIdentification = root.Element("ProfileIdentification")?.Value,
                ProfileRevision = root.Element("ProfileRevision")?.Value,
                ProfileName = root.Element("ProfileName")?.Value,
                ProfileSource = root.Element("ProfileSource")?.Value,
                ProfileClassId = root.Element("ProfileClassID")?.Value
            };

            var isoElement = root.Element("ISO15745Reference");
            if (isoElement == null)
                return headerObj;

            headerObj.Iso15745Part = isoElement.Element("ISO15745Part")?.Value;
            headerObj.Iso15745Edition = isoElement.Element("ISO15745Edition")?.Value;
            headerObj.ProfileTechnology = isoElement.Element("ProfileTechnology")?.Value;
            
            return headerObj;
        }

        /// <summary>
        /// Creates a single ExternalInterface element.
        /// </summary>
        /// <param name="withRefBaseClassPath">Specifies whether to add a RefBaseClassPath attribute.</param>
        /// <param name="version">The IODD version attribute.</param>
        /// <returns>The constructed ExternalInterface element.</returns>
        private static XElement CreateExternalInterface(bool withRefBaseClassPath, string version)
        {
            var root = XmlHelper.CreateElement("ExternalInterface");
            
            root.SetAttributeValue("Name", $"IODD {version}");
            root.SetAttributeValue("ID", "GUID");
            
            if(withRefBaseClassPath)
                root.SetAttributeValue("RefBaseClassPath", "AutomationMLInterfaceClassLib/AutomationMLBaseInterface/ExternalDataConnector");

            var attr = XmlHelper.CreateElement(root, "Attribute");
            attr.SetAttributeValue("Name", "refURI");
            attr.SetAttributeValue("AttributeDataType", "xs:anyURI");

            XmlHelper.CreateElement(attr, "Value", "/Balluff-BNI_IOL-302-000-K006-20150130-IODD1.1.xml");
            return root;
        }

        /// <summary>
        /// Constructs the InternalElement tag.
        /// </summary>
        /// <param name="obj">The profile header information from the IODD.</param>
        /// <returns>The internal element as a xml element.</returns>
        private static XElement ConstructInternalElement(ProfileHeaderObj obj)
        {
            var internalElement = XmlHelper.CreateElement("InternalElement");
            internalElement.SetAttributeValue("Name", "IODD");
            internalElement.SetAttributeValue("ID", "GUID");

            var supportedRoleClass = XmlHelper.CreateElement(internalElement, "SupportedRoleClass");
            supportedRoleClass.SetAttributeValue("RefRoleClassPath", "AutomationMLComponentBaseRCL/AdditionalDeviceDescription");

            var firstExternal = CreateExternalInterface(false, "1.1");
            var secondExternal = CreateExternalInterface(true, "1.0.1");
            
            internalElement.Add(firstExternal);
            internalElement.Add(secondExternal);

            return internalElement;
        }
        
        /// <inheritdoc />
        public bool CanApplyRule(XElement element)
        {
            return element.Name.LocalName == "ProfileHeader";
        }

        /// <inheritdoc />
        public XElement Apply(XElement element)
        {
            if(!CanApplyRule(element))
                throw new InvalidOperationException();

            var profileHeaderObj = ExtractHeaderInformation(element);
            return ConstructInternalElement(profileHeaderObj);
        }
        
    }
    
}