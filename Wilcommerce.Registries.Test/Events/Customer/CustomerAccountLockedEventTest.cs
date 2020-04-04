using System;
using Wilcommerce.Registries.Events.Customer;
using Xunit;

namespace Wilcommerce.Registries.Test.Events.Customer
{
    public class CustomerAccountLockedEventTest
    {
        [Fact]
        public void Ctor_Should_Set_All_Values_Correctly()
        {
            Guid customerId = Guid.NewGuid();
            string userName = "username";

            var ev = new CustomerAccountLockedEvent(customerId, userName);

            Assert.Equal(customerId, ev.CustomerId);
            Assert.Equal(userName, ev.UserName);
            Assert.Equal(customerId, ev.AggregateId);
            Assert.Equal(typeof(Registries.Models.Customer), ev.AggregateType);
        }

        [Fact]
        public void ToString_Should_Return_The_Event_Formatted_As_String()
        {
            Guid customerId = Guid.NewGuid();
            string userName = "username";

            var ev = new CustomerAccountLockedEvent(customerId, userName);

            string eventAsString = $"Account {userName} locked for customer {customerId}";

            Assert.Equal(eventAsString, ev.ToString());
        }
    }
}
