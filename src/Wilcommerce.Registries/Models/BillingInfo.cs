using System;

namespace Wilcommerce.Registries.Models
{
    /// <summary>
    /// Represent a customer billing information
    /// </summary>
    public class BillingInfo
    {
        /// <summary>
        /// Get or set the billing information id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Get or set the billing address
        /// </summary>
        public PostalAddress BillingAddress { get; set; }

        /// <summary>
        /// Get or set the customer full name used for billing
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Get or set the customer national identification number
        /// </summary>
        public string NationalIdentificationNumber { get; set; }

        /// <summary>
        /// Get or set the customer vat number
        /// </summary>
        public string VatNumber { get; set; }

        /// <summary>
        /// Get or set whether the current billing info is the default used
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
