using System;
using System.Collections.Generic;
using System.Text;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Registries.Events.Customer
{
    /// <summary>
    /// Company legal address changed
    /// </summary>
    public class CompanyLegalAddressChangedEvent : DomainEvent
    {
        /// <summary>
        /// Get the company id
        /// </summary>
        public Guid CompanyId { get; private set; }

        /// <summary>
        /// Get the address
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Get the address city
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// Get the address postal code
        /// </summary>
        public string PostalCode { get; private set; }

        /// <summary>
        /// Get the address province
        /// </summary>
        public string Province { get; private set; }

        /// <summary>
        /// Get the address country
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="companyId">The company id</param>
        /// <param name="address">The company address</param>
        /// <param name="city">The company city</param>
        /// <param name="postalCode">The company postal code</param>
        /// <param name="province">The company province</param>
        /// <param name="country">The company country</param>
        public CompanyLegalAddressChangedEvent(Guid companyId, string address, string city, string postalCode, string province, string country)
            : base(companyId, typeof(Models.Customer))
        {
            CompanyId = companyId;
            Address = address;
            City = city;
            PostalCode = postalCode;
            Province = province;
            Country = country;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Company {CompanyId} legal address change. {Address}, {City}, {PostalCode}, {Province}, {Country}";
        }
    }
}
