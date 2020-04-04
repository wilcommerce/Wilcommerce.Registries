using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Registries.Events.Customer
{
    /// <summary>
    /// Billing info marked as default
    /// </summary>
    public class BillingInformationMarkedAsDefaultEvent : DomainEvent
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
        /// Construct the event
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="billingInfoId">The billing info id</param>
        public BillingInformationMarkedAsDefaultEvent(Guid customerId, Guid billingInfoId)
            : base(customerId, typeof(Models.Customer))
        {
            CustomerId = customerId;
            BillingInfoId = billingInfoId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Billing info {BillingInfoId} marked as default for customer {CustomerId}";
        }
    }
}
