namespace AMLRider.Library.Iodd
{
    
    public class SupportedAccessLocks
    {
        
        /// <summary>
        /// Specifies whether parameter access lock is supported.
        /// </summary>
        public bool Parameter { get; set; }
        
        /// <summary>
        /// Specifies whether data storage access lock is supported.
        /// </summary>
        public bool DataStorage { get; set; }
        
        /// <summary>
        /// Specifies whether local parameterization access lock is supported.
        /// </summary>
        public bool LocalParameterization { get; set; }
        
        /// <summary>
        /// Specifies whether local user interface access lock is supported.
        /// </summary>
        public bool LocalUserInterface { get; set; }
        
    }
    
}