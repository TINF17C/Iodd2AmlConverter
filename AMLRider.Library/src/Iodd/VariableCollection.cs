using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace AMLRider.Library.Iodd
{
    
    public class VariableCollection : IEnumerable<Variable>
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
    }
    
}