using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Registries.Events.Customer.Handlers
{
    /// <summary>
    /// Handles all the event related to billing information
    /// </summary>
    public class BillingInfoEventHandler :
        IHandleEvent<BillingInformationAddedEvent>,
        IHandleEvent<BillingInformationChangedEvent>,
        IHandleEvent<BillingInformationMarkedAsDefaultEvent>,
        IHandleEvent<BillingoInformationRemovedEvent>
    {
        /// <summary>
        /// Get the event store
        /// </summary>
        public IEventStore EventStore { get; }

        /// <summary>
        /// Construct the handler
        /// </summary>
        /// <param name="eventStore">The event store</param>
        public BillingInfoEventHandler(IEventStore eventStore)
        {
            EventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
        /// </summary>
        /// <param name="event"></param>
        public void Handle(BillingInformationAddedEvent @event)
        {
            try
            {
                EventStore.Save(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
        /// </summary>
        /// <param name="event"></param>
        public void Handle(BillingInformationChangedEvent @event)
        {
            try
            {
                EventStore.Save(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
        /// </summary>
        /// <param name="event"></param>
        public void Handle(BillingInformationMarkedAsDefaultEvent @event)
        {
            try
            {
                EventStore.Save(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
        /// </summary>
        /// <param name="event"></param>
        public void Handle(BillingoInformationRemovedEvent @event)
        {
            try
            {
                EventStore.Save(@event);
            }
            catch
            {
                throw;
            }
        }
    }
}
