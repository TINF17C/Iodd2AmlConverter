using System;

namespace AMLRider.Library.Iodd
{
    
    public class DocumentInfo
    {
        
        /// <summary>
        /// Version of the concrete instance, not of the IODD specification.
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// The release date of the IO-Link device.
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        
        /// <summary>
        /// The vendor specific copyright text.
        /// </summary>
        public string Copyright { get; set; }
        
    }
    
}