using System;
using Wilcommerce.Registries.Events.Customer;
using Xunit;

namespace Wilcommerce.Registries.Test.Events.Customer
{
    public class CompanyLegalAddressChangedEventTest
    {
        [Fact]
        public void Ctor_Should_Set_All_Values_Correctly()
        {
            Guid companyId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";

            var ev = new CompanyLegalAddressChangedEvent(companyId, address, city, postalCode, province, country);

            Assert.Equal(companyId, ev.CompanyId);
            Assert.Equal(address, ev.Address);
            Assert.Equal(city, ev.City);
            Assert.Equal(postalCode, ev.PostalCode);
            Assert.Equal(province, ev.Province);
            Assert.Equal(country, ev.Country);

            Assert.Equal(companyId, ev.AggregateId);
            Assert.Equal(typeof(Registries.Models.Customer), ev.AggregateType);
        }

        [Fact]
        public void ToString_Should_Return_Event_Formatted_As_String()
        {
            Guid companyId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";

            var ev = new CompanyLegalAddressChangedEvent(companyId, address, city, postalCode, province, country);

            string eventAsString = $"Company {companyId} legal address change. {address}, {city}, {postalCode}, {province}, {country}";

            Assert.Equal(eventAsString, ev.ToString());
        }
    }
}
