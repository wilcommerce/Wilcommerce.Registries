using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Registries.Events.Customer
{
    /// <summary>
    /// Billing info changed
    /// </summary>
    public class BillingInformationChangedEvent : DomainEvent
    {
        /// <summary>
        /// Get the customer id
        /// </summary>
        public Guid CustomerId { get; private set; }

        /// <summary>
        /// Get the billing info id
        /// </summary>
        public Guid BillingInfoId { get; private set; }

        /// <summary>
        /// Get the full name
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// Get the billing information address
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Get the billing information city
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// Get the billing information postal code
        /// </summary>
        public string PostalCode { get; private set; }

        /// <summary>
        /// Get the billing information province
        /// </summary>
        public string Province { get; private set; }

        /// <summary>
        /// Get the billing information country
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// Get the national identification number
        /// </summary>
        public string NationalIdentificationNumber { get; private set; }

        /// <summary>
        /// Get the vat number
        /// </summary>
        public string VatNumber { get; private set; }

        /// <summary>
        /// Indicate whether the billing information is the default one
        /// </summary>
        public bool IsDefault { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="billingInfoId"></param>
        /// <param name="fullName">The full name</param>
        /// <param name="address">The billing information address</param>
        /// <param name="city">The billing information city</param>
        /// <param name="postalCode">The billing information postal code</param>
        /// <param name="province">The billing information province</param>
        /// <param name="country">The billing information country</param>
        /// <param name="nationalIdentificationNumber">The national identification number</param>
        /// <param name="vatNumber">The vat number</param>
        /// <param name="isDefault">Indicate whether the billing information is the default one</param>
        public BillingInformationChangedEvent(Guid customerId, Guid billingInfoId, string fullName, string address, string city, string postalCode, string province, string country, string nationalIdentificationNumber, string vatNumber, bool isDefault)
            : base(customerId, typeof(Models.Customer))
        {
            CustomerId = customerId;
            BillingInfoId = billingInfoId;
            FullName = fullName;
            Address = address;
            City = city;
            PostalCode = postalCode;
            Province = province;
            Country = country;
            NationalIdentificationNumber = nationalIdentificationNumber;
            VatNumber = vatNumber;
            IsDefault = isDefault;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Billing info {BillingInfoId} changed for customer {CustomerId} with values {FullName}, {NationalIdentificationNumber} {VatNumber}, {Address} {City}, {PostalCode}, {Province} {Country}";
        }
    }
}
