using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Registries.Events.Customer
{
    /// <summary>
    /// Company registered
    /// </summary>
    public class CompanyRegisteredEvent : DomainEvent
    {
        /// <summary>
        /// Get the company id
        /// </summary>
        public Guid CompanyId { get; private set; }

        /// <summary>
        /// Get the company name
        /// </summary>
        public string CompanyName { get; private set; }

        /// <summary>
        /// Get the company vat number
        /// </summary>
        public string VatNumber { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="companyId">The company id</param>
        /// <param name="companyName">The company name</param>
        /// <param name="vatNumber">The company vat number</param>
        public CompanyRegisteredEvent(Guid companyId, string companyName, string vatNumber)
            : base(companyId, typeof(Models.Customer))
        {
            CompanyId = companyId;
            CompanyName = companyName;
            VatNumber = vatNumber;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Company {CompanyName}, VAT {VatNumber} registered";
        }
    }
}
