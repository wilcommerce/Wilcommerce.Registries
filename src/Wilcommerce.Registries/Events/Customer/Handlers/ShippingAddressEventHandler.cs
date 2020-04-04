using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Registries.Events.Customer.Handlers
{
    /// <summary>
    /// Handles all the events related to shipping address
    /// </summary>
    public class ShippingAddressEventHandler :
        IHandleEvent<ShippingAddressAddedEvent>,
        IHandleEvent<ShippingAddressChangedEvent>,
        IHandleEvent<ShippingAddressMarkedAsDefaultEvent>,
        IHandleEvent<ShippingAddressRemovedEvent>
    {
        /// <summary>
        /// Get the event store
        /// </summary>
        public IEventStore EventStore { get; }

        /// <summary>
        /// Construct the handler
        /// </summary>
        /// <param name="eventStore">The event store</param>
        public ShippingAddressEventHandler(IEventStore eventStore)
        {
            EventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
        /// </summary>
        /// <param name="event"></param>
        public void Handle(ShippingAddressAddedEvent @event)
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
        public void Handle(ShippingAddressChangedEvent @event)
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
        public void Handle(ShippingAddressMarkedAsDefaultEvent @event)
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
        public void Handle(ShippingAddressRemovedEvent @event)
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
