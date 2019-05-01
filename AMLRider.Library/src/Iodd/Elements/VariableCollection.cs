using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;
using AMLRider.Library.Extensions;

namespace AMLRider.Library.Iodd.Elements
{
    public class VariableCollection : IoddElement, IEnumerable<Variable>, IEnumerable<StdVariableRef>
    {
        private List<Variable> Variables { get; }

        private List<StdVariableRef> StdVariableRefs { get; set; }

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

        public override AmlElement ToAml()
        {
            var element = new InternalElement
            {
                Name = "VariableCollection",
                Id = "VariableCollection"
            };

            foreach (var variable in Variables)
            {
                var amlElement = variable.ToAml() as InternalElement;
                element.InternalElements.Add(amlElement);
            }

            foreach (var variableRef in StdVariableRefs)
            {
                var amlElement = variableRef.ToAml() as InternalElement;
                element.InternalElements.Add(amlElement);
            }

            return element;
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