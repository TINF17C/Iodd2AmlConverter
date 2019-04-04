namespace AMLRider.Library.Iodd
{
    public class DeviceVariant
    {

        #region Attributes
        
        /// <summary>
        /// The product ID.
        /// </summary>
        public int ProductId { get; set; }
        
        public string DeviceSymbol { get; set; }
        
        public string DeviceIcon { get; set; }

        #endregion

        #region Elements

        public string Name { get; set; }
        
        public string Description { get; set; }

        #endregion
        
    }
}