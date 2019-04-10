using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using AMLRider.Library.Aml;

namespace AMLRider.Library.Iodd.Elements
{
    
    public class VariableCollection : IoddElement, IEnumerable<Variable>
    {
        
        public StdVariableRef StdVariableRef { get; set; }
        
        private List<Variable> Variables { get; }

        public VariableCollection()
        {
            Variables = new List<Variable>();
        }

        public void Add(Variable variable)
        {
            Variables.Add(variable);
        }

        public Variable Get(int index)
        {
            return Variables[index];
        }

        public IEnumerator<Variable> GetEnumerator()
        {
            return Variables.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override void Deserialize(XElement element)
        {
            throw new System.NotImplementedException();
        }

        public override AmlElement ToAml()
        {
            throw new System.NotImplementedException();
        }
        
    }
    
}