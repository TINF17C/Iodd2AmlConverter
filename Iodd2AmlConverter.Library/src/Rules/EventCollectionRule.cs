using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Helpers;

namespace Iodd2AmlConverter.Library.Rules
{   
    /// <inheritdoc />
    /// <summary>
    /// Responsible for the conversion of the EventCollection.
    /// </summary>
    public class EventCollectionRule : IConversionRule
    {
        /// <inheritdoc />
        /// <summary>
        /// Checks if the rule can be applied on the node.
        /// </summary>
        /// <param name="element">A given XML element.</param>
        /// <returns>True, if the rule can be applied. False, if not.</returns>
        public bool CanApplyRule(XElement element)
        {
            return element.Name.LocalName == "EventCollection";
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates the EventCollectionTag.
        /// </summary>
        /// <param name="element">The given IODD element</param>
        /// <returns>The created AML element</returns>
        /// <exception cref="T:System.InvalidOperationException"></exception>
        public XElement Apply(XElement element)
        {
            if(!CanApplyRule(element))
                throw new InvalidOperationException("The given node is not a <EventCollection> node.");

            var eventCodes = GetEventCodes(element);
            return CreateEventCollectionTag(eventCodes);
        }

        /// <summary>
        /// Gets the event codes from the IODD element.
        /// </summary>
        /// <param name="element">The given IODD element.</param>
        /// <returns>An integer array which contains the eventCodes.</returns>
        private static int[] GetEventCodes(XElement element)
        {
            var subElements = element.Elements();
            IList<int> eventCodes = subElements.Select(subElement => int.Parse(subElement.GetAttributeValue("code"))).ToList();

            return eventCodes.ToArray();
        }
        
        /// <summary>
        /// Creates the AML EventCollection element.
        /// </summary>
        /// <param name="eventCodes">An integer array with the eventCodes.</param>
        /// <returns>The created AML element</returns>
        private static XElement CreateEventCollectionTag(int[] eventCodes)
        {
            var internalElement = XmlHelper.CreateElement("InternalElement");
            internalElement.SetAttributeValue("Name", "EventCollection");
            internalElement.SetAttributeValue("ID", "EventCollection");
            var refSemantic = XmlHelper.CreateElement(internalElement, "RefSemantic");
            refSemantic.SetAttributeValue("CorrespondingAttributePath", "ListType");
            foreach (var eventCode in eventCodes)
            {
                var attribute = XmlHelper.CreateElement(internalElement, "Attribute");
                attribute.SetAttributeValue("Name", eventCode.ToString());
                attribute.SetAttributeValue("AttributeDataType", "xs:integer");
                var defaultValue = XmlHelper.CreateElement(attribute, "Defaultvalue", "0");
            }


            return internalElement;
        }
    }
}