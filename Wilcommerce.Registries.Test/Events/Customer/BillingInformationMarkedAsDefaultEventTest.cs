using System;
using System.Collections.Generic;
using System.Text;
using Wilcommerce.Registries.Events.Customer;
using Xunit;

namespace Wilcommerce.Registries.Test.Events.Customer
{
    public class BillingInformationMarkedAsDefaultEventTest
    {
        [Fact]
        public void Ctor_Should_Set_All_Values_Correctly()
        {
            Guid customerId = Guid.NewGuid();
            Guid billingInfoId = Guid.NewGuid();

            var ev = new BillingInformationMarkedAsDefaultEvent(customerId, billingInfoId);

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

            var ev = new BillingInformationMarkedAsDefaultEvent(customerId, billingInfoId);

            string eventAsString = $"Billing info {billingInfoId} marked as default for customer {customerId}";

            Assert.Equal(eventAsString, ev.ToString());
        }
    }
}
