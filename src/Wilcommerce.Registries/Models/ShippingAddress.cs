using System;

namespace Wilcommerce.Registries.Models
{
    /// <summary>
    /// Represents a shipping address
    /// </summary>
    public class ShippingAddress
    {
        #region Properties
        /// <summary>
        /// Get or set the id of the shipping address
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Get or set the address information
        /// </summary>
        public virtual PostalAddress AddressInfo { get; set; }

        /// <summary>
        /// Get or set whether is the default shipping address
        /// </summary>
        public bool IsDefault { get; set; }
        #endregion
    }
}
