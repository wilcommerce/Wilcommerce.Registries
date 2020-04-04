using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Registries.Events.Customer
{
    /// <summary>
    /// Shipping address added
    /// </summary>
    public class ShippingAddressAddedEvent : DomainEvent
    {
        /// <summary>
        /// Get the customer id
        /// </summary>
        public Guid CustomerId { get; private set; }

        /// <summary>
        /// Get the shipping address
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Get the shipping address city
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// Get the shipping address postal code
        /// </summary>
        public string PostalCode { get; private set; }

        /// <summary>
        /// Get the shipping address province
        /// </summary>
        public string Province { get; private set; }

        /// <summary>
        /// Get the shipping address country
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// Indicate whether the shipping address is the default one
        /// </summary>
        public bool IsDefault { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="address">The shipping address</param>
        /// <param name="city">The shipping address city</param>
        /// <param name="postalCode">The shipping address postal code</param>
        /// <param name="province">The shipping address province</param>
        /// <param name="country">The shipping address country</param>
        /// <param name="isDefault">Indicate whether the shipping address is the default one</param>
        public ShippingAddressAddedEvent(Guid customerId, string address, string city, string postalCode, string province, string country, bool isDefault)
            : base(customerId, typeof(Models.Customer))
        {
            CustomerId = customerId;
            Address = address;
            City = city;
            PostalCode = postalCode;
            Province = province;
            Country = country;
            IsDefault = isDefault;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Shipping address {Address}, {City} {PostalCode}, {Province} {Country} added to customer {CustomerId}";
        }
    }
}
