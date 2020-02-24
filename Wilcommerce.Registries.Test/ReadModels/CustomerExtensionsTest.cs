using System;
using System.Linq;
using Wilcommerce.Registries.Models;
using Wilcommerce.Registries.ReadModels;
using Xunit;

namespace Wilcommerce.Registries.Test.ReadModels
{
    public class CustomerExtensionsTest
    {
        #region Active tests
        [Fact]
        public void Active_Should_Throw_ArgumentNullException_If_Customers_Is_Null()
        {
            IQueryable<Customer> customers = null;

            var ex = Assert.Throws<ArgumentNullException>(() => CustomerExtensions.Active(customers));
            Assert.Equal(nameof(customers), ex.ParamName);
        }

        [Fact]
        public void Active_Should_Return_Only_Customers_Not_Deleted()
        {
            var customer1 = Person.Register("firstName", "lastname", Gender.Female, new DateTime(1980, 1, 1));
            var customer2 = Person.Register("firstName2", "lastname2", Gender.Male, new DateTime(1980, 1, 1));
            var customer3 = Company.Register("company", "1234567890");

            customer2.Delete();

            IQueryable<Customer> customers = new Customer[]
            {
                customer1, customer2, customer3
            }.AsQueryable();

            var activeCustomers = CustomerExtensions.Active(customers);

            Assert.True(activeCustomers.All(c => !c.Deleted));
        }
        #endregion

        #region People tests
        [Fact]
        public void People_Should_Throw_ArgumentNullException_If_Customers_Is_Null()
        {
            IQueryable<Customer> customers = null;

            var ex = Assert.Throws<ArgumentNullException>(() => CustomerExtensions.People(customers));
            Assert.Equal(nameof(customers), ex.ParamName);
        }

        [Fact]
        public void People_Should_Return_Only_Customer_Of_Type_Person()
        {
            var customer1 = Person.Register("firstName", "lastname", Gender.Female, new DateTime(1980, 1, 1));
            var customer2 = Person.Register("firstName2", "lastname2", Gender.Male, new DateTime(1980, 1, 1));
            var customer3 = Company.Register("company", "1234567890");

            IQueryable<Customer> customers = new Customer[]
            {
                customer1, customer2, customer3
            }.AsQueryable();

            var people = CustomerExtensions.People(customers);

            Assert.True(people.All(p => p is Person));
        }
        #endregion

        #region Companies tests
        [Fact]
        public void Companies_Should_Throw_ArgumentNullException_If_Customers_Is_Null()
        {
            IQueryable<Customer> customers = null;

            var ex = Assert.Throws<ArgumentNullException>(() => CustomerExtensions.Companies(customers));
            Assert.Equal(nameof(customers), ex.ParamName);
        }

        [Fact]
        public void Companies_Should_Return_Only_Customers_Of_Type_Company()
        {
            var customer1 = Person.Register("firstName", "lastname", Gender.Female, new DateTime(1980, 1, 1));
            var customer2 = Person.Register("firstName2", "lastname2", Gender.Male, new DateTime(1980, 1, 1));
            var customer3 = Company.Register("company", "1234567890");

            IQueryable<Customer> customers = new Customer[]
            {
                customer1, customer2, customer3
            }.AsQueryable();

            var people = CustomerExtensions.Companies(customers);

            Assert.True(people.All(p => p is Company));
        }
        #endregion
    }
}
