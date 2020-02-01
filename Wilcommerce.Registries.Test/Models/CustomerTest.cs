using System;
using System.Linq;
using Wilcommerce.Registries.Models;
using Xunit;

namespace Wilcommerce.Registries.Test.Models
{
    public class CustomerTest
    {
        #region Delete test
        [Fact]
        public void Delete_Should_Throw_InvalidOperationException_If_Customer_Is_Already_Deleted()
        {
            var customer = this.CreateCustomerMock();
            customer.Delete();

            var ex = Assert.Throws<InvalidOperationException>(() => customer.Delete());
            Assert.Equal("Customer already deleted", ex.Message);
        }

        [Fact]
        public void Delete_Should_Mark_The_Customer_As_Deleted()
        {
            var customer = this.CreateCustomerMock();
            customer.Delete();

            Assert.True(customer.Deleted);
        }
        #endregion

        #region Restore test
        [Fact]
        public void Restore_Should_Throw_InvalidOperationException_If_Customer_Is_Not_Deleted()
        {
            var customer = this.CreateCustomerMock();

            var ex = Assert.Throws<InvalidOperationException>(() => customer.Restore());
            Assert.Equal("Customer is not deleted", ex.Message);
        }
        
        [Fact]
        public void Restore_Should_Mark_Customer_As_Not_Deleted()
        {
            var customer = this.CreateCustomerMock();
            customer.Delete();

            customer.Restore();

            Assert.False(customer.Deleted);
        }
        #endregion

        #region SetAccount test
        [Fact]
        public void SetAccount_Should_Set_Account_Info_With_Specified_Values()
        {
            var customer = this.CreateCustomerMock();

            Guid userId = Guid.NewGuid();
            string userName = "username";

            customer.SetAccount(userId, userName);

            Assert.Equal(userId, customer.Account?.UserId);
            Assert.Equal(userName, customer.Account?.UserName);
        }
        #endregion

        #region RemoveAccount test
        [Fact]
        public void RemoveAccount_Should_Set_Account_Info_As_Empty()
        {
            var customer = this.CreateCustomerMock();
            customer.SetAccount(Guid.NewGuid(), "user");

            customer.RemoveAccount();

            Assert.Equal(AccountInfo.EmptyAccount(), customer.Account);
        }
        #endregion

        #region LockAccount test
        [Fact]
        public void LockAccount_Should_Throw_InvalidOperationException_If_Customer_Has_Not_Any_Account_Associated()
        {
            var customer = this.CreateCustomerMock();

            var ex = Assert.Throws<InvalidOperationException>(() => customer.LockAccount());
            Assert.Equal("Customer hasn't any account associated", ex.Message);
        }

        [Fact]
        public void LockAccount_Should_Mark_Account_As_Locked()
        {
            var customer = this.CreateCustomerMock();
            customer.SetAccount(Guid.NewGuid(), "user");

            customer.LockAccount();

            Assert.True(customer.Account.IsLocked);
        }
        #endregion

        #region UnlockAccount test
        [Fact]
        public void UnlockAccount_Should_Throw_InvalidOperationException_If_Customer_Has_Not_Any_Account_Associated()
        {
            var customer = this.CreateCustomerMock();

            var ex = Assert.Throws<InvalidOperationException>(() => customer.UnlockAccount());
            Assert.Equal("Customer hasn't any account associated", ex.Message);
        }

        [Fact]
        public void UnlockAccount_Should_Throw_InvalidOperationException_If_Customer_Account_Is_Not_Locked()
        {
            var customer = this.CreateCustomerMock();
            customer.SetAccount(Guid.NewGuid(), "user");

            var ex = Assert.Throws<InvalidOperationException>(() => customer.UnlockAccount());
            Assert.Equal("Customer account is not locked", ex.Message);
        }

        [Fact]
        public void UnlockAccount_Should_Mark_Account_As_Not_Locked()
        {
            var customer = this.CreateCustomerMock();
            customer.SetAccount(Guid.NewGuid(), "user");
            customer.LockAccount();

            customer.UnlockAccount();

            Assert.False(customer.Account.IsLocked);
        }
        #endregion

        #region SetNationalIdentificationNumber test
        [Fact]
        public void SetNationalIdentificationNumber_Should_Set_The_National_Identification_Number_With_The_Specified_Value()
        {
            var customer = this.CreateCustomerMock();

            string nationalIdentificationNumber = "nationalidentificationnumber";
            customer.SetNationalIdentificationNumber(nationalIdentificationNumber);

            Assert.Equal(nationalIdentificationNumber, customer.NationalIdentificationNumber);
        }
        #endregion

        #region AddShippingAddress test
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void AddShippingAddress_Should_Throw_ArgumentException_If_Address_Is_Empty(string address)
        {
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "Italy";
            bool isDefault = false;

            var customer = this.CreateCustomerMock();

            var ex = Assert.Throws<ArgumentException>(() => customer.AddShippingAddress(address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(address), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void AddShippingAddress_Should_Throw_ArgumentException_If_City_Is_Empty(string city)
        {
            string address = "address";
            string postalCode = "12345";
            string province = "province";
            string country = "Italy";
            bool isDefault = false;

            var customer = this.CreateCustomerMock();

            var ex = Assert.Throws<ArgumentException>(() => customer.AddShippingAddress(address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(city), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void AddShippingAddress_Should_Throw_ArgumentException_If_Province_Is_Empty(string province)
        {
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string country = "Italy";
            bool isDefault = false;

            var customer = this.CreateCustomerMock();

            var ex = Assert.Throws<ArgumentException>(() => customer.AddShippingAddress(address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(province), ex.ParamName);
        }

        [Fact]
        public void AddShippingAddress_Should_Reset_The_Previous_Default_Address_If_IsDefault_Is_True()
        {
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "Italy";
            bool isDefault = true;

            var customer = this.CreateCustomerMock();
            customer.AddShippingAddress("a1", "c1", "11111", "p", "Italy", true);

            customer.AddShippingAddress(address, city, postalCode, province, country, isDefault);

            var defaultAddress = customer.ShippingAddresses.SingleOrDefault(a => a.IsDefault);
            
            Assert.NotNull(defaultAddress);
            Assert.Equal(address, defaultAddress.AddressInfo.Address);
            Assert.Equal(city, defaultAddress.AddressInfo.City);
            Assert.Equal(postalCode, defaultAddress.AddressInfo.PostalCode);
            Assert.Equal(province, defaultAddress.AddressInfo.Province);
            Assert.Equal(country, defaultAddress.AddressInfo.Country);
        }

        [Fact]
        public void AddShippingAddress_Should_Add_A_New_Address_With_Specified_Values()
        {
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "Italy";
            bool isDefault = true;

            var customer = this.CreateCustomerMock();
            customer.AddShippingAddress(address, city, postalCode, province, country, isDefault);

            Assert.Collection(customer.ShippingAddresses, a =>
            {
                Assert.Equal(address, a.AddressInfo.Address);
                Assert.Equal(city, a.AddressInfo.City);
                Assert.Equal(postalCode, a.AddressInfo.PostalCode);
                Assert.Equal(province, a.AddressInfo.Province);
                Assert.Equal(country, a.AddressInfo.Country);
                Assert.Equal(isDefault, a.IsDefault);
            });
        }
        #endregion

        #region ChangeShippingAddress test
        [Fact]
        public void ChangeShippingAddress_Should_Throw_ArgumentException_If_AddressId_Is_Empty()
        {
            Guid addressId = Guid.Empty;
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "Italy";
            bool isDefault = false;

            var customer = this.CreateCustomerMock();

            var ex = Assert.Throws<ArgumentException>(() => customer.ChangeShippingAddress(addressId, address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(addressId), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ChangeShippingAddress_Should_Throw_ArgumentException_If_Address_Is_Empty(string address)
        {
            Guid addressId = Guid.NewGuid();
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "Italy";
            bool isDefault = false;

            var customer = this.CreateCustomerMock();

            var ex = Assert.Throws<ArgumentException>(() => customer.ChangeShippingAddress(addressId, address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(address), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ChangeShippingAddress_Should_Throw_ArgumentException_If_City_Is_Empty(string city)
        {
            Guid addressId = Guid.NewGuid();
            string address = "address";
            string postalCode = "12345";
            string province = "province";
            string country = "Italy";
            bool isDefault = false;

            var customer = this.CreateCustomerMock();

            var ex = Assert.Throws<ArgumentException>(() => customer.ChangeShippingAddress(addressId, address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(city), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ChangeShippingAddress_Should_Throw_ArgumentException_If_Province_Is_Empty(string province)
        {
            Guid addressId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string country = "Italy";
            bool isDefault = false;

            var customer = this.CreateCustomerMock();

            var ex = Assert.Throws<ArgumentException>(() => customer.ChangeShippingAddress(addressId, address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(province), ex.ParamName);
        }

        [Fact]
        public void ChangeShippingAddress_Should_Throw_ArgumentOutOfRangeException_If_Shipping_Address_Is_Not_Found()
        {
            Guid addressId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "Italy";
            bool isDefault = false;

            var customer = this.CreateCustomerMock();

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => customer.ChangeShippingAddress(addressId, address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(addressId), ex.ParamName);
        }

        [Fact]
        public void ChangeShippingAddress_Should_Reset_The_Previous_Default_Address_If_IsDefault_Is_True()
        {
            var customer = this.CreateCustomerMock();
            customer.AddShippingAddress("a1", "c1", "11111", "p", "Italy", false);
            customer.AddShippingAddress("a2", "c2", "22222", "p", "Italy", true);

            Guid addressId = customer.ShippingAddresses.First(a => !a.IsDefault).Id;
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "Italy";
            bool isDefault = true;

            customer.ChangeShippingAddress(addressId, address, city, postalCode, province, country, isDefault);

            var defaultAddress = customer.ShippingAddresses.SingleOrDefault(a => a.IsDefault);

            Assert.NotNull(defaultAddress);
            Assert.Equal(addressId, defaultAddress.Id);
            Assert.Equal(address, defaultAddress.AddressInfo.Address);
            Assert.Equal(city, defaultAddress.AddressInfo.City);
            Assert.Equal(postalCode, defaultAddress.AddressInfo.PostalCode);
            Assert.Equal(province, defaultAddress.AddressInfo.Province);
            Assert.Equal(country, defaultAddress.AddressInfo.Country);
        }

        [Fact]
        public void ChangeShippingAddress_Should_Change_Shipping_Address_With_Specified_Values()
        {
            var customer = this.CreateCustomerMock();
            customer.AddShippingAddress("a1", "c1", "11111", "p", "Italy", true);

            Guid addressId = customer.ShippingAddresses.First().Id;
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "Italy";
            bool isDefault = true;

            customer.ChangeShippingAddress(addressId, address, city, postalCode, province, country, isDefault);

            Assert.Collection(customer.ShippingAddresses, a =>
            {
                Assert.Equal(addressId, a.Id);
                Assert.Equal(address, a.AddressInfo.Address);
                Assert.Equal(city, a.AddressInfo.City);
                Assert.Equal(postalCode, a.AddressInfo.PostalCode);
                Assert.Equal(province, a.AddressInfo.Province);
                Assert.Equal(country, a.AddressInfo.Country);
                Assert.Equal(isDefault, a.IsDefault);
            });
        }
        #endregion

        #region RemoveShippingAddress test
        [Fact]
        public void RemoveShippingAddress_Should_Throw_ArgumentException_If_AddressId_Is_Empty()
        {
            var customer = this.CreateCustomerMock();
            customer.AddShippingAddress("address", "city", "12345", "province", "country", false);

            Guid addressId = Guid.Empty;

            var ex = Assert.Throws<ArgumentException>(() => customer.RemoveShippingAddress(addressId));
            Assert.Equal(nameof(addressId), ex.ParamName);
        }

        [Fact]
        public void RemoveShippingAddress_Should_Throw_ArgumentOutOfRangeException_If_Shipping_Address_Is_Not_Found()
        {
            var customer = this.CreateCustomerMock();
            customer.AddShippingAddress("address", "city", "12345", "province", "country", false);

            Guid addressId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => customer.RemoveShippingAddress(addressId));
            Assert.Equal(nameof(addressId), ex.ParamName);
        }

        [Fact]
        public void RemoveShippingAddress_Should_Throw_InvalidOperationException_If_Shipping_Address_Is_The_Default_Address()
        {
            var customer = this.CreateCustomerMock();
            customer.AddShippingAddress("address", "city", "12345", "province", "country", true);

            Guid addressId = customer.ShippingAddresses.First().Id;

            var ex = Assert.Throws<InvalidOperationException>(() => customer.RemoveShippingAddress(addressId));
            Assert.Equal("Cannot remove the shipping default address", ex.Message);
        }

        [Fact]
        public void RemoveShippingAddress_Should_Remove_The_Address_From_The_List()
        {
            var customer = this.CreateCustomerMock();
            customer.AddShippingAddress("address", "city", "12345", "province", "country", false);

            Guid addressId = customer.ShippingAddresses.First().Id;
            customer.RemoveShippingAddress(addressId);

            Assert.True(customer.ShippingAddresses.All(a => a.Id != addressId));
        }
        #endregion

        #region MarkShippingAddressAsDefault test
        [Fact]
        public void MarkShippingAddressAsDefault_Should_Throw_ArgumentException_If_AddressId_Is_Empty()
        {
            var customer = this.CreateCustomerMock();
            customer.AddShippingAddress("address", "city", "12345", "province", "country", false);

            Guid addressId = Guid.Empty;

            var ex = Assert.Throws<ArgumentException>(() => customer.MarkShippingAddressAsDefault(addressId));
            Assert.Equal(nameof(addressId), ex.ParamName);
        }

        [Fact]
        public void MarkShippingAddressAsDefault_Should_Throw_ArgumentOutOfRangeException_If_Shipping_Address_Is_Not_Found()
        {
            var customer = this.CreateCustomerMock();
            customer.AddShippingAddress("address", "city", "12345", "province", "country", false);

            Guid addressId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => customer.MarkShippingAddressAsDefault(addressId));
            Assert.Equal(nameof(addressId), ex.ParamName);
        }

        [Fact]
        public void MarkShippingAddressAsDefault_Should_Reset_The_Default_Shipping_Address_If_Is_Different_From_The_Specified_Address()
        {
            var customer = this.CreateCustomerMock();
            customer.AddShippingAddress("a", "c", "11111", "p", "Italy", true);
            customer.AddShippingAddress("address", "city", "12345", "province", "country", false);

            Guid addressId = customer.ShippingAddresses.First(a => !a.IsDefault).Id;
            customer.MarkShippingAddressAsDefault(addressId);

            var defaultAddress = customer.ShippingAddresses.SingleOrDefault(a => a.IsDefault);
            Assert.Equal(addressId, defaultAddress.Id);
        }

        [Fact]
        public void MarkShippingAddressAsDefault_Should_Set_The_Specified_Address_As_Default_Shipping_Address()
        {
            var customer = this.CreateCustomerMock();
            customer.AddShippingAddress("address", "city", "12345", "province", "country", false);

            Guid addressId = customer.ShippingAddresses.First().Id;
            customer.MarkShippingAddressAsDefault(addressId);

            var address = customer.ShippingAddresses.SingleOrDefault(a => a.Id == addressId);
            Assert.True(address.IsDefault);
        }
        #endregion

        #region Private customer mock
        private Customer CreateCustomerMock()
        {
            var customer = Person.Register("name", "lastname", Gender.Male, new DateTime(1987, 1, 1));
            return customer;
        }
        #endregion
    }
}
