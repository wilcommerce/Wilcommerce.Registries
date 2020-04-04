using System;
using Wilcommerce.Registries.Events.Customer;
using Xunit;

namespace Wilcommerce.Registries.Test.Events.Customer
{
    public class BillingInformationChangedEventTest
    {
        [Fact]
        public void Ctor_Should_Set_All_Values_Correctly()
        {
            Guid customerId = Guid.NewGuid();
            Guid billingInfoId = Guid.NewGuid();
            string fullName = "fullname";
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "country";
            string nationalIdentificationNumber = "number";
            string vatNumber = "1234567890";
            bool isDefault = true;

            var ev = new BillingInformationChangedEvent(customerId, billingInfoId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault);

            Assert.Equal(customerId, ev.CustomerId);
            Assert.Equal(billingInfoId, ev.BillingInfoId);
            Assert.Equal(fullName, ev.FullName);
            Assert.Equal(address, ev.Address);
            Assert.Equal(city, ev.City);
            Assert.Equal(postalCode, ev.PostalCode);
            Assert.Equal(province, ev.Province);
            Assert.Equal(country, ev.Country);
            Assert.Equal(nationalIdentificationNumber, ev.NationalIdentificationNumber);
            Assert.Equal(vatNumber, ev.VatNumber);
            Assert.Equal(isDefault, ev.IsDefault);

            Assert.Equal(customerId, ev.AggregateId);
            Assert.Equal(typeof(Registries.Models.Customer), ev.AggregateType);
        }

        [Fact]
        public void ToString_Should_Return_Event_Formatted_As_String()
        {
            Guid customerId = Guid.NewGuid();
            Guid billingInfoId = Guid.NewGuid();
            string fullName = "fullname";
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "country";
            string nationalIdentificationNumber = "number";
            string vatNumber = "1234567890";
            bool isDefault = true;

            var ev = new BillingInformationChangedEvent(customerId, billingInfoId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault);

            string eventAsString = $"Billing info {billingInfoId} changed for customer {customerId} with values {fullName}, {nationalIdentificationNumber} {vatNumber}, {address} {city}, {postalCode}, {province} {country}";

            Assert.Equal(eventAsString, ev.ToString());
        }
    }
}
