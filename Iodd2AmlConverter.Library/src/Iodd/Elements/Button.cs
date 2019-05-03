using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    public class Button : IoddElement
    {
        public int ButtonValue { get; set; }
        public string Description { get; set; }
        public string ActionStartedMessage { get; set; }
        
        public override void Deserialize(XElement element)
        {
            ButtonValue = int.Parse(element.GetAttributeValue("buttonValue"));
            if (element.SubElement("Description") != null)
                Description = element.SubElement("Description").GetAttributeValue("textId");
            if (element.SubElement("ActionStartedMessage") != null)
                ActionStartedMessage = element.SubElement("ActionStartedMessage").GetAttributeValue("textId");
        }

        public override AmlElement ToAml()
        {
            var button = new Attribute();
            button.Name = "Button";
            var buttonValue = new Attribute();
            if (Description != null)
                buttonValue.Name = "aml-text=" + Description;
            buttonValue.AttributeDataType = "xs:integer";
            var value = new Value
            {
                Content = ButtonValue.ToString()
            };
            buttonValue.Value = value;
            button.Attributes.Add(buttonValue);
            var actionStartedMessage = new Attribute();
            var actionStartedMessageSubelement = new Attribute();
            if (ActionStartedMessage != null)
                actionStartedMessageSubelement.Name = "aml-text=" + ActionStartedMessage;
            actionStartedMessage.Attributes.Add(actionStartedMessageSubelement);
            button.Attributes.Add(actionStartedMessage);

            return button;
        }
    }
}