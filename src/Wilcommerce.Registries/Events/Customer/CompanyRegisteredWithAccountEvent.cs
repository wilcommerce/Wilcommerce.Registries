using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Registries.Events.Customer
{
    /// <summary>
    /// Company registered with account
    /// </summary>
    public class CompanyRegisteredWithAccountEvent : DomainEvent
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
        /// Get the company username
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="companyId">The company id</param>
        /// <param name="companyName">The company name</param>
        /// <param name="vatNumber">The company vat number</param>
        /// <param name="userName">The company username</param>
        public CompanyRegisteredWithAccountEvent(Guid companyId, string companyName, string vatNumber, string userName)
            : base(companyId, typeof(Models.Customer))
        {
            CompanyId = companyId;
            CompanyName = companyName;
            VatNumber = vatNumber;
            UserName = userName;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Company {CompanyName}, VAT {VatNumber}, User name {UserName} registered";
        }
    }
}
