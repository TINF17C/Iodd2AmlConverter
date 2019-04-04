using System.Collections.Generic;
using AMLRider.Library.Utils;

namespace AMLRider.Library.Iodd
{
    public class DeviceFunction
    {
        
        public Features Features { get; set; }
        
        public Optional<DataTypeCollection> DataTypes { get; set; }
        
        public VariableCollection Variables { get; set; }

    }
}