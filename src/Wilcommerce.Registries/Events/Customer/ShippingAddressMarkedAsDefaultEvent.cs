using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Registries.Events.Customer
{
    /// <summary>
    /// Shipping address marked as default
    /// </summary>
    public class ShippingAddressMarkedAsDefaultEvent : DomainEvent
    {
        /// <summary>
        /// Get the customer id
        /// </summary>
        public Guid CustomerId { get; private set; }

        /// <summary>
        /// Get the address id
        /// </summary>
        public Guid AddressId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="addressId">The address id</param>
        public ShippingAddressMarkedAsDefaultEvent(Guid customerId, Guid addressId)
            : base(customerId, typeof(Models.Customer))
        {
            CustomerId = customerId;
            AddressId = addressId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Address {AddressId} marked as default for customer {CustomerId}";
        }
    }
}
