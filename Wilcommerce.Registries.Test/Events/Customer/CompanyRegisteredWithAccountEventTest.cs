using System;
using Wilcommerce.Registries.Events.Customer;
using Xunit;

namespace Wilcommerce.Registries.Test.Events.Customer
{
    public class CompanyRegisteredWithAccountEventTest
    {
        [Fact]
        public void Ctor_Should_Set_All_Values_Correctly()
        {
            Guid companyId = Guid.NewGuid();
            string companyName = "company";
            string vatNumber = "1234567890";
            string userName = "username";

            var ev = new CompanyRegisteredWithAccountEvent(companyId, companyName, vatNumber, userName);

            Assert.Equal(companyId, ev.CompanyId);
            Assert.Equal(companyName, ev.CompanyName);
            Assert.Equal(vatNumber, ev.VatNumber);
            Assert.Equal(userName, ev.UserName);

            Assert.Equal(companyId, ev.AggregateId);
            Assert.Equal(typeof(Registries.Models.Customer), ev.AggregateType);
            Assert.Equal(DateTime.Today, ev.FiredOn.Date);
        }

        [Fact]
        public void ToString_Should_Return_Event_Formatted_As_String()
        {
            Guid companyId = Guid.NewGuid();
            string companyName = "company";
            string vatNumber = "1234567890";
            string userName = "username";

            var ev = new CompanyRegisteredWithAccountEvent(companyId, companyName, vatNumber, userName);

            string eventAsString = $"Company {companyName}, VAT {vatNumber}, User name {userName} registered";

            Assert.Equal(eventAsString, ev.ToString());
        }
    }
}
