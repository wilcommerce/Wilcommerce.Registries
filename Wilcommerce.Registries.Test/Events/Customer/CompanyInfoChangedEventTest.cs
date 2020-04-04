using System;
using System.Collections.Generic;
using System.Text;
using Wilcommerce.Registries.Events.Customer;
using Xunit;

namespace Wilcommerce.Registries.Test.Events.Customer
{
    public class CompanyInfoChangedEventTest
    {
        [Fact]
        public void Ctor_Should_Set_All_Values_Correctly()
        {
            Guid companyId = Guid.NewGuid();
            string companyName = "company";
            string vatNumber = "1234567890";
            string nationalIdentificationNumber = "number";

            var ev = new CompanyInfoChangedEvent(companyId, companyName, vatNumber, nationalIdentificationNumber);

            Assert.Equal(companyId, ev.CompanyId);
            Assert.Equal(companyName, ev.CompanyName);
            Assert.Equal(vatNumber, ev.VatNumber);
            Assert.Equal(nationalIdentificationNumber, ev.NationalIdentificationNumber);

            Assert.Equal(companyId, ev.AggregateId);
            Assert.Equal(typeof(Registries.Models.Customer), ev.AggregateType);
        }

        [Fact]
        public void ToString_Should_Return_Event_Formatted_As_String()
        {
            Guid companyId = Guid.NewGuid();
            string companyName = "company";
            string vatNumber = "1234567890";
            string nationalIdentificationNumber = "number";

            var ev = new CompanyInfoChangedEvent(companyId, companyName, vatNumber, nationalIdentificationNumber);

            string eventAsString = $"Company {companyId} info changed. Company name: {companyName}, VAT number: {vatNumber}, National identification number: {nationalIdentificationNumber}";

            Assert.Equal(eventAsString, ev.ToString());
        }
    }
}
