using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Extensions;
using Iodd2AmlConverter.Library.Aml;
using Iodd2AmlConverter.Library.Aml.Elements;

namespace Iodd2AmlConverter.Library.Iodd.Elements
{
    public class VariableCollection : IoddElement, IEnumerable<Variable>, IEnumerable<StdVariableRef>
    {
        public List<Variable> Variables { get; }

        public List<StdVariableRef> StdVariableRefs { get; set; }

        public VariableCollection()
        {
            Variables = new List<Variable>();
            StdVariableRefs = new List<StdVariableRef>();
        }

        public override void Deserialize(XElement element)
        {
            foreach (var variableElement in element.SubElements("Variable"))
            {
                var variable = new Variable();
                variable.Deserialize(variableElement);
                
                Variables.Add(variable);
            }
            
            foreach (var variableRefElement in element.SubElements("StdVariableRef"))
            {
                var variableRef = new StdVariableRef();
                variableRef.Deserialize(variableRefElement);
                
                StdVariableRefs.Add(variableRef);
            }
        }

        public override AmlCollection ToAml()
        {
            var element = new InternalElement
            {
                Name = "VariableCollection",
                Id = "VariableCollection"
            };

            foreach (var variable in Variables)
            {
                var amlElement = variable.ToAml().Cast<InternalElement>();
                element.InternalElements.AddRange(amlElement);
            }

            foreach (var variableRef in StdVariableRefs)
            {
                var amlElement = variableRef.ToAml().Cast<InternalElement>();
                element.InternalElements.AddRange(amlElement);
            }

            return AmlCollection.Of(element);
        }

        IEnumerator<StdVariableRef> IEnumerable<StdVariableRef>.GetEnumerator()
        {
            return StdVariableRefs.GetEnumerator();
        }

        public IEnumerator<Variable> GetEnumerator()
        {
            return Variables.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}