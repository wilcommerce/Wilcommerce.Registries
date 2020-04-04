using System;
using Wilcommerce.Registries.Events.Customer;
using Xunit;

namespace Wilcommerce.Registries.Test.Events.Customer
{
    public class ShippingAddressAddedEventTest
    {
        [Fact]
        public void Ctor_Should_Set_All_Values_Correctly()
        {
            Guid customerId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "country";
            bool isDefault = true;

            var ev = new ShippingAddressAddedEvent(customerId, address, city, postalCode, province, country, isDefault);

            Assert.Equal(customerId, ev.CustomerId);
            Assert.Equal(address, ev.Address);
            Assert.Equal(city, ev.City);
            Assert.Equal(postalCode, ev.PostalCode);
            Assert.Equal(province, ev.Province);
            Assert.Equal(country, ev.Country);
            Assert.Equal(isDefault, ev.IsDefault);

            Assert.Equal(customerId, ev.AggregateId);
            Assert.Equal(typeof(Registries.Models.Customer), ev.AggregateType);
        }

        [Fact]
        public void ToString_Should_Return_Event_Formatted_As_String()
        {
            Guid customerId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "country";
            bool isDefault = true;

            var ev = new ShippingAddressAddedEvent(customerId, address, city, postalCode, province, country, isDefault);

            string eventAsString = $"Shipping address {address}, {city} {postalCode}, {province} {country} added to customer {customerId}";

            Assert.Equal(eventAsString, ev.ToString());
        }
    }
}
