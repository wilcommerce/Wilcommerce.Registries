using Moq;
using System;
using System.Threading.Tasks;
using Wilcommerce.Registries.Commands;
using Wilcommerce.Registries.Models;
using Wilcommerce.Registries.Repository;
using Xunit;

namespace Wilcommerce.Registries.Test.Commands
{
    public class CustomerCommandsTest
    {
        #region Constrcutor tests
        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_Repository_Is_Null()
        {
            IRepository repository = null;

            var ex = Assert.Throws<ArgumentNullException>(() => new CustomerCommands(repository));
            Assert.Equal(nameof(repository), ex.ParamName);
        }
        #endregion

        #region AddCustomerBillingInformation tests
        [Fact]
        public async Task AddCustomerBillingInformation_Should_Throw_ArgumentException_If_CustomerId_Is_Empty()
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.Empty;
            string fullName = "fullname";
            string address = "address"; 
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            string nationalIdentificationNumber = "";
            string vatNumber = "1234567890";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddCustomerBillingInformation(customerId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task AddCustomerBillingInformation_Should_Throw_ArgumentException_If_FullName_Is_Empty(string fullName)
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            string nationalIdentificationNumber = "";
            string vatNumber = "1234567890";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddCustomerBillingInformation(customerId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault));
            Assert.Equal(nameof(fullName), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task AddCustomerBillingInformation_Should_Throw_ArgumentException_If_Address_Is_Empty(string address)
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string fullName = "full name";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            string nationalIdentificationNumber = "";
            string vatNumber = "1234567890";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddCustomerBillingInformation(customerId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault));
            Assert.Equal(nameof(address), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task AddCustomerBillingInformation_Should_Throw_ArgumentException_If_City_Is_Empty(string city)
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string fullName = "full name";
            string address = "address";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            string nationalIdentificationNumber = "";
            string vatNumber = "1234567890";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddCustomerBillingInformation(customerId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault));
            Assert.Equal(nameof(city), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task AddCustomerBillingInformation_Should_Throw_ArgumentException_If_Province_Is_Empty(string province)
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string fullName = "full name";
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string country = "italy";
            string nationalIdentificationNumber = "";
            string vatNumber = "1234567890";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddCustomerBillingInformation(customerId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault));
            Assert.Equal(nameof(province), ex.ParamName);
        }

        [Fact]
        public async Task AddCustomerBillingInformation_Should_Throw_ArgumentOutOfRangeException_If_Customer_Is_Not_Found()
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string fullName = "full name";
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            string nationalIdentificationNumber = "";
            string vatNumber = "1234567890";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => commands.AddCustomerBillingInformation(customerId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task AddCustomerBillingInformation_Should_Add_New_Billing_Information_With_Specified_Values()
        {
            Customer customer = Company.Register("Company", "1234567890");

            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Customer>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(customer));

            var repository = repositoryMock.Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string fullName = "full name";
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            string nationalIdentificationNumber = "";
            string vatNumber = "1234567890";
            bool isDefault = true;

            await commands.AddCustomerBillingInformation(customerId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault);

            Assert.Collection(customer.BillingInformation, b =>
            {
                Assert.Equal(fullName, b.FullName);
                Assert.Equal(address, b.BillingAddress.Address);
                Assert.Equal(city, b.BillingAddress.City);
                Assert.Equal(postalCode, b.BillingAddress.PostalCode);
                Assert.Equal(province, b.BillingAddress.Province);
                Assert.Equal(country, b.BillingAddress.Country);
                Assert.Equal(nationalIdentificationNumber, b.NationalIdentificationNumber);
                Assert.Equal(vatNumber, b.VatNumber);
                Assert.Equal(isDefault, b.IsDefault);
            });
        }
        #endregion

        #region AddCustomerShippingAddress tests
        [Fact]
        public async Task AddCustomerShippingAddress_Should_Throw_ArgumentException_If_CustomerId_Is_Empty()
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.Empty;
            string address = "address";
            string city = "city";
            string postalCode = "postalCode";
            string province = "province";
            string country = "italy";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddCustomerShippingAddress(customerId, address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task AddCustomerShippingAddress_Should_Throw_ArgumentException_If_Address_Is_Empty(string address)
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string city = "city";
            string postalCode = "postalCode";
            string province = "province";
            string country = "italy";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddCustomerShippingAddress(customerId, address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(address), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task AddCustomerShippingAddress_Should_Throw_ArgumentException_If_City_Is_Empty(string city)
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string address = "address";
            string postalCode = "postalCode";
            string province = "province";
            string country = "italy";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddCustomerShippingAddress(customerId, address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(city), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task AddCustomerShippingAddress_Should_Throw_ArgumentException_If_Province_Is_Empty(string province)
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "postalCode";
            string country = "italy";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddCustomerShippingAddress(customerId, address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(province), ex.ParamName);
        }

        [Fact]
        public async Task AddCustomerShippingAddress_Should_Throw_ArgumentOutOfRangeException_If_Customer_Is_Not_Found()
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "postalCode";
            string province = "province";
            string country = "italy";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => commands.AddCustomerShippingAddress(customerId, address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task AddCustomerShippingAddress_Should_Add_New_Shipping_Address_With_Specified_Values()
        {
            Customer customer = Company.Register("company", "1234567890");

            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Customer>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(customer));

            var repository = repositoryMock.Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "postalCode";
            string province = "province";
            string country = "italy";
            bool isDefault = true;

            await commands.AddCustomerShippingAddress(customerId, address, city, postalCode, province, country, isDefault);

            Assert.Collection(customer.ShippingAddresses, s =>
            {
                Assert.Equal(address, s.AddressInfo.Address);
                Assert.Equal(city, s.AddressInfo.City);
                Assert.Equal(postalCode, s.AddressInfo.PostalCode);
                Assert.Equal(province, s.AddressInfo.Province);
                Assert.Equal(country, s.AddressInfo.Country);
                Assert.Equal(isDefault, s.IsDefault);
            });
        }
        #endregion

        #region ChangeCompanyInfo tests
        [Fact]
        public async Task ChangeCompanyInfo_Should_Throw_ArgumentException_If_CustomerId_Is_Empty()
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.Empty;
            string companyName = "Company";
            string vatNumber = "1234567890";
            string nationalIdentificationNumber = "1234567890";

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCompanyInfo(customerId, companyName, vatNumber, nationalIdentificationNumber));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ChangeCompanyInfo_Should_Throw_ArgumentException_If_CompanyName_Is_Empty(string companyName)
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string vatNumber = "1234567890";
            string nationalIdentificationNumber = "1234567890";

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCompanyInfo(customerId, companyName, vatNumber, nationalIdentificationNumber));
            Assert.Equal(nameof(companyName), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ChangeCompanyInfo_Should_Throw_ArgumentException_If_VatNumber_Is_Empty(string vatNumber)
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string companyName = "Company";
            string nationalIdentificationNumber = "1234567890";

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCompanyInfo(customerId, companyName, vatNumber, nationalIdentificationNumber));
            Assert.Equal(nameof(vatNumber), ex.ParamName);
        }

        [Fact]
        public async Task ChangeCompanyInfo_Should_Throw_ArgumentOutOfRangeException_If_Customer_Is_Not_Found()
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string companyName = "Company";
            string vatNumber = "1234567890";
            string nationalIdentificationNumber = "1234567890";

            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => commands.ChangeCompanyInfo(customerId, companyName, vatNumber, nationalIdentificationNumber));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task ChangeCompanyInfo_Should_Change_Info_With_Specified_Values()
        {
            var customer = Company.Register("my company", "0123456789");

            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Company>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(customer));

            var repository = repositoryMock.Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = customer.Id;
            string companyName = "Company";
            string vatNumber = "1234567890";
            string nationalIdentificationNumber = "1234567890";

            await commands.ChangeCompanyInfo(customerId, companyName, vatNumber, nationalIdentificationNumber);

            Assert.Equal(companyName, customer.CompanyName);
            Assert.Equal(vatNumber, customer.VatNumber);
            Assert.Equal(nationalIdentificationNumber, customer.NationalIdentificationNumber);
        }
        #endregion

        #region ChangeCompanyLegalAddress tests
        [Fact]
        public async Task ChangeCompanyLegalAddress_Should_Throw_ArgumentException_If_CustomerId_Is_Empty()
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.Empty;
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCompanyLegalAddress(customerId, address, city, postalCode, province, country));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ChangeCompanyLegalAddress_Should_Throw_ArgumentException_If_Address_Is_Empty(string address)
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCompanyLegalAddress(customerId, address, city, postalCode, province, country));
            Assert.Equal(nameof(address), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ChangeCompanyLegalAddress_Should_Throw_ArgumentException_If_City_Is_Empty(string city)
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string address = "address";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCompanyLegalAddress(customerId, address, city, postalCode, province, country));
            Assert.Equal(nameof(city), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ChangeCompanyLegalAddress_Should_Throw_ArgumentException_If_Province_Is_Empty(string province)
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string country = "italy";

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCompanyLegalAddress(customerId, address, city, postalCode, province, country));
            Assert.Equal(nameof(province), ex.ParamName);
        }

        [Fact]
        public async Task ChangeCompanyLegalAddress_Should_Throw_ArgumentOutOfRangeException_If_Customer_Is_Not_Found()
        {
            var repository = new Mock<IRepository>().Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";

            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => commands.ChangeCompanyLegalAddress(customerId, address, city, postalCode, province, country));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task ChangeCompanyLegalAddress_Should_Change_Legal_Address_With_Specified_Values()
        {
            var customer = Company.Register("company", "1234567890");

            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(s => s.GetByKeyAsync<Company>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(customer));

            var repository = repositoryMock.Object;
            var commands = new CustomerCommands(repository);

            Guid customerId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";

            await commands.ChangeCompanyLegalAddress(customerId, address, city, postalCode, province, country);

            Assert.Equal(address, customer.LegalAddress.Address);
            Assert.Equal(city, customer.LegalAddress.City);
            Assert.Equal(postalCode, customer.LegalAddress.PostalCode);
            Assert.Equal(province, customer.LegalAddress.Province);
            Assert.Equal(country, customer.LegalAddress.Country);
        }
        #endregion

        #region ChangeCustomerBillingInformation tests
        #endregion

        #region ChangeCustomerShippingAddress tests
        #endregion

        #region ChangePersonInfo tests
        #endregion

        #region DeleteCustomer tests
        #endregion

        #region LockCustomerAccount tests
        #endregion

        #region MarkCustomerBillingInformationAsDefault tests
        #endregion

        #region MarkCustomerShippingAddressAsDefault tests
        #endregion

        #region RegisterNewCompany tests
        #endregion

        #region RegisterNewPerson tests
        #endregion

        #region RemoveCustomerAccount tests
        #endregion

        #region RemoveCustomerBillingInformation tests
        #endregion

        #region RemoveCustomerShippingAddress tests
        #endregion

        #region RestoreCustomer tests
        #endregion

        #region SetCustomerAccount tests
        #endregion
    }
}
