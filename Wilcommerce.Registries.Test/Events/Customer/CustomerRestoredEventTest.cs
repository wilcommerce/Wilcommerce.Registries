using System;
using Wilcommerce.Registries.Events.Customer;
using Xunit;

namespace Wilcommerce.Registries.Test.Events.Customer
{
    public class CustomerRestoredEventTest
    {
        [Fact]
        public void Ctor_Should_Set_All_Values_Correctly()
        {
            var customerId = Guid.NewGuid();
            var ev = new CustomerRestoredEvent(customerId);

            Assert.Equal(customerId, ev.CustomerId);
            Assert.Equal(customerId, ev.AggregateId);
            Assert.Equal(typeof(Registries.Models.Customer), ev.AggregateType);
        }

        [Fact]
        public void ToString_Should_Return_Event_Formatted_As_String()
        {
            var customerId = Guid.NewGuid();
            var ev = new CustomerRestoredEvent(customerId);

            string eventAsString = $"Customer {customerId} restored";

            Assert.Equal(eventAsString, ev.ToString());
        }
    }
}
