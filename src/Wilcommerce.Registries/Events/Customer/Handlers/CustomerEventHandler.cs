using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Registries.Events.Customer.Handlers
{
    /// <summary>
    /// Handles all the customer common events
    /// </summary>
    public class CustomerEventHandler :
        IHandleEvent<CustomerAccountLockedEvent>,
        IHandleEvent<CustomerAccountRemovedEvent>,
        IHandleEvent<CustomerAccountSetEvent>,
        IHandleEvent<CustomerDeletedEvent>,
        IHandleEvent<CustomerRestoredEvent>
    {
        /// <summary>
        /// Get the event store
        /// </summary>
        public IEventStore EventStore { get; }

        /// <summary>
        /// Construct the handler
        /// </summary>
        /// <param name="eventStore">The event store</param>
        public CustomerEventHandler(IEventStore eventStore)
        {
            EventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
        /// </summary>
        /// <param name="event"></param>
        public void Handle(CustomerAccountLockedEvent @event)
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
        public void Handle(CustomerAccountRemovedEvent @event)
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
        public void Handle(CustomerAccountSetEvent @event)
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
        public void Handle(CustomerDeletedEvent @event)
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
        public void Handle(CustomerRestoredEvent @event)
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
