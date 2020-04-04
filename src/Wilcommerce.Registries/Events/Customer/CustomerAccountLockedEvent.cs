using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Registries.Events.Customer
{
    /// <summary>
    /// Customer account locked
    /// </summary>
    public class CustomerAccountLockedEvent : DomainEvent
    {
        /// <summary>
        /// Get the customer id
        /// </summary>
        public Guid CustomerId { get; private set; }

        /// <summary>
        /// Get the customer user name
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="userName">The customer username</param>
        public CustomerAccountLockedEvent(Guid customerId, string userName)
            : base(customerId, typeof(Models.Customer))
        {
            CustomerId = customerId;
            UserName = userName;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Account {UserName} locked for customer {CustomerId}";
        }
    }
}
