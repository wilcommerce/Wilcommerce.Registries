using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Registries.Events.Customer
{
    /// <summary>
    /// Company info changed
    /// </summary>
    public class CompanyInfoChangedEvent : DomainEvent
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
        /// Get the company national identification number
        /// </summary>
        public string NationalIdentificationNumber { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="companyId">The company id</param>
        /// <param name="companyName">The company name</param>
        /// <param name="vatNumber">The company vat number</param>
        /// <param name="nationalIdentificationNumber">The company national identification number</param>
        public CompanyInfoChangedEvent(Guid companyId, string companyName, string vatNumber, string nationalIdentificationNumber)
            : base(companyId, typeof(Models.Customer))
        {
            CompanyId = companyId;
            CompanyName = companyName;
            VatNumber = vatNumber;
            NationalIdentificationNumber = nationalIdentificationNumber;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Company {CompanyId} info changed. Company name: {CompanyName}, VAT number: {VatNumber}, National identification number: {NationalIdentificationNumber}";
        }
    }
}
