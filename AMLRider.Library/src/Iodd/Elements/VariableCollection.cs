using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;

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
            foreach (var variableElement in element.Elements("Variable"))
            {
                var variable = new Variable();
                variable.Deserialize(variableElement);
                
                Variables.Add(variable);
            }
            
            foreach (var variableRefElement in element.Elements("StdVariableRef"))
            {
                var variableRef = new StdVariableRef();
                variableRef.Deserialize(variableRefElement);
                
                StdVariableRefs.Add(variableRef);
            }
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator<StdVariableRef> IEnumerable<StdVariableRef>.GetEnumerator()
        {
            throw new System.NotImplementedException();
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