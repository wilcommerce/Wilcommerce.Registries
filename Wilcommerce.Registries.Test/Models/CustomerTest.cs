using System;
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

        #region Private customer mock
        private Customer CreateCustomerMock()
        {
            var customer = Person.Register("name", "lastname", Gender.Male, new DateTime(1987, 1, 1));
            return customer;
        }
        #endregion
    }
}
