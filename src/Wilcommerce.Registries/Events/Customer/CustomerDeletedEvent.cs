﻿using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Registries.Events.Customer
{
    /// <summary>
    /// Customer deleted event
    /// </summary>
    public class CustomerDeletedEvent : DomainEvent
    {
        /// <summary>
        /// Get the customer id
        /// </summary>
        public Guid CustomerId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="customerId">The customer id</param>
        public CustomerDeletedEvent(Guid customerId)
            : base(customerId, typeof(Models.Customer))
        {
            CustomerId = customerId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Customer {CustomerId} deleted";
        }
    }
}
