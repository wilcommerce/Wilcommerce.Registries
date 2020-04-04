using System;
using Wilcommerce.Registries.Events.Customer;
using Xunit;

namespace Wilcommerce.Registries.Test.Events.Customer
{
    public class ShippingAddressMarkedAsDefaultEventTest
    {
        [Fact]
        public void Ctor_Should_Set_All_Values_Correctly()
        {
            Guid customerId = Guid.NewGuid();
            Guid addressId = Guid.NewGuid();

            var ev = new ShippingAddressMarkedAsDefaultEvent(customerId, addressId);

            Assert.Equal(customerId, ev.CustomerId);
            Assert.Equal(addressId, ev.AddressId);

            Assert.Equal(customerId, ev.AggregateId);
            Assert.Equal(typeof(Registries.Models.Customer), ev.AggregateType);
        }

        [Fact]
        public void ToString_Should_Return_Event_Formatted_As_String()
        {
            Guid customerId = Guid.NewGuid();
            Guid addressId = Guid.NewGuid();

            var ev = new ShippingAddressMarkedAsDefaultEvent(customerId, addressId);

            string eventAsString = $"Address {addressId} marked as default for customer {customerId}";

            Assert.Equal(eventAsString, ev.ToString());
        }
    }
}
