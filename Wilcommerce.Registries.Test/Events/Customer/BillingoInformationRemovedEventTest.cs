using System;
using Wilcommerce.Registries.Events.Customer;
using Xunit;

namespace Wilcommerce.Registries.Test.Events.Customer
{
    public class BillingoInformationRemovedEventTest
    {
        [Fact]
        public void Ctor_Should_Set_All_Values_Correctly()
        {
            Guid customerId = Guid.NewGuid();
            Guid billingInfoId = Guid.NewGuid();

            var ev = new BillingoInformationRemovedEvent(customerId, billingInfoId);

            Assert.Equal(customerId, ev.CustomerId);
            Assert.Equal(billingInfoId, ev.BillingInfoId);

            Assert.Equal(customerId, ev.AggregateId);
            Assert.Equal(typeof(Registries.Models.Customer), ev.AggregateType);
        }

        [Fact]
        public void ToString_Should_Return_Event_Formatted_As_String()
        {
            Guid customerId = Guid.NewGuid();
            Guid billingInfoId = Guid.NewGuid();

            var ev = new BillingoInformationRemovedEvent(customerId, billingInfoId);

            string eventAsString = $"Billing info {billingInfoId} removed from customer {customerId}";

            Assert.Equal(eventAsString, ev.ToString());
        }
    }
}
