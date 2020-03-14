using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Wilcommerce.Registries.Commands;
using Wilcommerce.Registries.Models;
using Wilcommerce.Registries.Repository;
using Wilcommerce.Registries.Services;
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
            IAuthClient authClient = new Mock<IAuthClient>().Object;

            var ex = Assert.Throws<ArgumentNullException>(() => new CustomerCommands(repository, authClient));
            Assert.Equal(nameof(repository), ex.ParamName);
        }

        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_AuthClient_Is_Null()
        {
            IRepository repository = new Mock<IRepository>().Object;
            IAuthClient authClient = null;

            var ex = Assert.Throws<ArgumentNullException>(() => new CustomerCommands(repository, authClient));
            Assert.Equal(nameof(authClient), ex.ParamName);
        }
        #endregion

        #region AddCustomerBillingInformation tests
        [Fact]
        public async Task AddCustomerBillingInformation_Should_Throw_ArgumentException_If_CustomerId_Is_Empty()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

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
        [Fact]
        public async Task ChangeCustomerBillingInformation_Should_Throw_ArgumentException_If_CustomerId_Is_Empty()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.Empty;
            Guid billingInfoId = Guid.NewGuid();
            string fullName = "full name";
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            string nationalIdentificationNumber = "";
            string vatNumber = "1234567890";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCustomerBillingInformation(customerId, billingInfoId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task ChangeCustomerBillingInformation_Should_Throw_ArgumentException_If_BillingInfoId_Is_Empty()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            Guid billingInfoId = Guid.Empty;
            string fullName = "full name";
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            string nationalIdentificationNumber = "";
            string vatNumber = "1234567890";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCustomerBillingInformation(customerId, billingInfoId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault));
            Assert.Equal(nameof(billingInfoId), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ChangeCustomerBillingInformation_Should_Throw_ArgumentException_If_FullName_Is_Empty(string fullName)
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            Guid billingInfoId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            string nationalIdentificationNumber = "";
            string vatNumber = "1234567890";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCustomerBillingInformation(customerId, billingInfoId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault));
            Assert.Equal(nameof(fullName), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ChangeCustomerBillingInformation_Should_Throw_ArgumentException_If_Address_Is_Empty(string address)
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            Guid billingInfoId = Guid.NewGuid();
            string fullName = "full name";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            string nationalIdentificationNumber = "";
            string vatNumber = "1234567890";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCustomerBillingInformation(customerId, billingInfoId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault));
            Assert.Equal(nameof(address), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ChangeCustomerBillingInformation_Should_Throw_ArgumentException_If_City_Is_Empty(string city)
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            Guid billingInfoId = Guid.NewGuid();
            string fullName = "full name";
            string address = "address";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            string nationalIdentificationNumber = "";
            string vatNumber = "1234567890";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCustomerBillingInformation(customerId, billingInfoId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault));
            Assert.Equal(nameof(city), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ChangeCustomerBillingInformation_Should_Throw_ArgumentException_If_Province_Is_Empty(string province)
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            Guid billingInfoId = Guid.NewGuid();
            string fullName = "full name";
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string country = "italy";
            string nationalIdentificationNumber = "";
            string vatNumber = "1234567890";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCustomerBillingInformation(customerId, billingInfoId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault));
            Assert.Equal(nameof(province), ex.ParamName);
        }

        [Fact]
        public async Task ChangeCustomerBillingInformation_Should_Throw_ArgumentOutOfRangeException_If_Customer_Is_Not_Found()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            Guid billingInfoId = Guid.NewGuid();
            string fullName = "full name";
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            string nationalIdentificationNumber = "";
            string vatNumber = "1234567890";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => commands.ChangeCustomerBillingInformation(customerId, billingInfoId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task ChangeCustomerBillingInformation_Should_Change_Billing_Information_With_Specified_Values()
        {
            Customer customer = Company.Register("Company", "1234567890");
            customer.AddBillingInformation("my name", "address1", "city1", "00000", "province", "italy", "", "0987654321", true);

            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Customer>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(customer));

            var repository = repositoryMock.Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = customer.Id;
            Guid billingInfoId = customer.BillingInformation.First().Id;
            string fullName = "full name";
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            string nationalIdentificationNumber = "";
            string vatNumber = "1234567890";
            bool isDefault = true;

            await commands.ChangeCustomerBillingInformation(customerId, billingInfoId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault);

            var billingInfo = customer.BillingInformation.FirstOrDefault(b => b.Id == billingInfoId);
            
            Assert.NotNull(billingInfo);
            Assert.Equal(fullName, billingInfo.FullName);
            Assert.Equal(address, billingInfo.BillingAddress.Address);
            Assert.Equal(city, billingInfo.BillingAddress.City);
            Assert.Equal(postalCode, billingInfo.BillingAddress.PostalCode);
            Assert.Equal(province, billingInfo.BillingAddress.Province);
            Assert.Equal(country, billingInfo.BillingAddress.Country);
            Assert.Equal(nationalIdentificationNumber, billingInfo.NationalIdentificationNumber);
            Assert.Equal(vatNumber, billingInfo.VatNumber);
            Assert.Equal(isDefault, billingInfo.IsDefault);
        }
        #endregion

        #region ChangeCustomerShippingAddress tests
        [Fact]
        public async Task ChangeCustomerShippingAddress_Should_Throw_ArgumentException_If_CustomerId_Is_Empty()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.Empty;
            Guid addressId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCustomerShippingAddress(customerId, addressId, address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task ChangeCustomerShippingAddress_Should_Throw_ArgumentException_If_AddressId_Is_Empty()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            Guid addressId = Guid.Empty;
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCustomerShippingAddress(customerId, addressId, address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(addressId), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ChangeCustomerShippingAddress_Should_Throw_ArgumentException_If_Address_Is_Empty(string address)
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            Guid addressId = Guid.NewGuid();
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCustomerShippingAddress(customerId, addressId, address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(address), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ChangeCustomerShippingAddress_Should_Throw_ArgumentException_If_City_Is_Empty(string city)
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            Guid addressId = Guid.NewGuid();
            string address = "address";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCustomerShippingAddress(customerId, addressId, address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(city), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ChangeCustomerShippingAddress_Should_Throw_ArgumentException_If_Province_Is_Empty(string province)
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            Guid addressId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string country = "italy";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeCustomerShippingAddress(customerId, addressId, address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(province), ex.ParamName);
        }

        [Fact]
        public async Task ChangeCustomerShippingAddress_Should_Throw_ArgumentOutOfRangeException_If_Customer_Is_Not_Found()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            Guid addressId = Guid.NewGuid();
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            bool isDefault = true;

            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => commands.ChangeCustomerShippingAddress(customerId, addressId, address, city, postalCode, province, country, isDefault));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task ChangeCustomerShippingAddress_Should_Change_Shipping_Address_With_Specified_Values()
        {
            Customer customer = Person.Register("Name", "lastname", Gender.Female, new DateTime(1980, 1, 1));
            customer.AddShippingAddress("address1", "city1", "00000", "province", "italy", true);

            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Customer>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(customer));

            var repository = repositoryMock.Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = customer.Id;
            Guid addressId = customer.ShippingAddresses.First().Id;
            string address = "address";
            string city = "city";
            string postalCode = "12345";
            string province = "province";
            string country = "italy";
            bool isDefault = true;

            await commands.ChangeCustomerShippingAddress(customerId, addressId, address, city, postalCode, province, country, isDefault);

            var shippingAddress = customer.ShippingAddresses.FirstOrDefault(s => s.Id == addressId);

            Assert.NotNull(shippingAddress);
            Assert.Equal(address, shippingAddress.AddressInfo.Address);
            Assert.Equal(city, shippingAddress.AddressInfo.City);
            Assert.Equal(postalCode, shippingAddress.AddressInfo.PostalCode);
            Assert.Equal(province, shippingAddress.AddressInfo.Province);
            Assert.Equal(country, shippingAddress.AddressInfo.Country);
            Assert.Equal(isDefault, shippingAddress.IsDefault);
        }
        #endregion

        #region ChangePersonInfo tests
        [Fact]
        public async Task ChangePersonInfo_Should_Throw_ArgumentException_If_CustomerId_Is_Empty()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.Empty;
            string firstName = "first";
            string lastName = "lastname";
            string nationalIdentificationNumber = "number";
            Gender gender = Gender.Female;
            DateTime birthDate = new DateTime(1980, 1, 1);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangePersonInfo(customerId, firstName, lastName, nationalIdentificationNumber, gender, birthDate));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ChangePersonInfo_Should_Throw_ArgumentException_If_FirstName_Is_Empty(string firstName)
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            string lastName = "lastname";
            string nationalIdentificationNumber = "number";
            Gender gender = Gender.Female;
            DateTime birthDate = new DateTime(1980, 1, 1);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangePersonInfo(customerId, firstName, lastName, nationalIdentificationNumber, gender, birthDate));
            Assert.Equal(nameof(firstName), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ChangePersonInfo_Should_Throw_ArgumentException_If_LastName_Is_Empty(string lastName)
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            string firstName = "first";
            string nationalIdentificationNumber = "number";
            Gender gender = Gender.Female;
            DateTime birthDate = new DateTime(1980, 1, 1);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangePersonInfo(customerId, firstName, lastName, nationalIdentificationNumber, gender, birthDate));
            Assert.Equal(nameof(lastName), ex.ParamName);
        }

        [Fact]
        public async Task ChangePersonInfo_Should_Throw_ArgumentOutOfRangeException_If_Customer_Is_Not_Found()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            string firstName = "first";
            string lastName = "lastname";
            string nationalIdentificationNumber = "number";
            Gender gender = Gender.Female;
            DateTime birthDate = new DateTime(1980, 1, 1);

            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => commands.ChangePersonInfo(customerId, firstName, lastName, nationalIdentificationNumber, gender, birthDate));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task ChangePersonInfo_Should_Change_Info_With_Specified_Values()
        {
            var customer = Person.Register("name", "surname", Gender.Female, new DateTime(1980, 1, 3));
            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Person>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(customer));

            var repository = repositoryMock.Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = customer.Id;
            string firstName = "first";
            string lastName = "lastname";
            string nationalIdentificationNumber = "number";
            Gender gender = Gender.Female;
            DateTime birthDate = new DateTime(1980, 1, 1);

            await commands.ChangePersonInfo(customerId, firstName, lastName, nationalIdentificationNumber, gender, birthDate);

            Assert.Equal(firstName, customer.FirstName);
            Assert.Equal(lastName, customer.LastName);
            Assert.Equal(nationalIdentificationNumber, customer.NationalIdentificationNumber);
            Assert.Equal(gender, customer.Gender);
            Assert.Equal(birthDate, customer.BirthDate);
        }
        #endregion

        #region DeleteCustomer tests
        [Fact]
        public async Task DeleteCustomer_Should_Throw_ArgumentException_If_CustomerId_Is_Empty()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.Empty;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.DeleteCustomer(customerId));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task DeleteCustomer_Should_Throw_ArgumentOutOfRangeException_If_Customer_Is_Not_Found()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();

            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => commands.DeleteCustomer(customerId));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task DeleteCustomer_Should_Mark_Customer_As_Deleted()
        {
            Customer customer = Company.Register("company", "1234567890");

            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Customer>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(customer));

            var repository = repositoryMock.Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = customer.Id;

            await commands.DeleteCustomer(customerId);

            Assert.True(customer.Deleted);
        }
        #endregion

        #region LockCustomerAccount tests
        [Fact]
        public async Task LockCustomerAccount_Should_Throw_ArgumentException_If_CustomerId_Is_Empty()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.Empty;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.LockCustomerAccount(customerId));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task LockCustomerAccount_Should_Throw_ArgumentOutOfRangeException_If_Customer_Is_Not_Found()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();

            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => commands.LockCustomerAccount(customerId));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task LockCustomerAccount_Should_Lock_Customer_Account()
        {
            Customer customer = Company.RegisterWithAccount("company", "1234567890", Guid.NewGuid(), "username");
            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Customer>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(customer));

            var authClientMock = new Mock<IAuthClient>();

            var repository = repositoryMock.Object;
            var authClient = authClientMock.Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            await commands.LockCustomerAccount(customerId);

            authClientMock.Verify(a => a.DisableAccount(customer.Account.UserId));
            Assert.True(customer.Account.IsLocked);
        }
        #endregion

        #region MarkCustomerBillingInformationAsDefault tests
        [Fact]
        public async Task MarkCustomerBillingInformationAsDefault_Should_Throw_ArgumentException_If_CustomerId_Is_Empty()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;

            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.Empty;
            Guid billingInfoId = Guid.NewGuid();

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.MarkCustomerBillingInformationAsDefault(customerId, billingInfoId));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task MarkCustomerBillingInformationAsDefault_Should_Throw_ArgumentException_If_BillingInfoId_Is_Empty()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;

            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            Guid billingInfoId = Guid.Empty;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.MarkCustomerBillingInformationAsDefault(customerId, billingInfoId));
            Assert.Equal(nameof(billingInfoId), ex.ParamName);
        }

        [Fact]
        public async Task MarkCustomerBillingInformationAsDefault_Should_Throw_ArgumentOutOfRangeException_If_Customer_Is_Not_Found()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;

            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            Guid billingInfoId = Guid.NewGuid();

            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => commands.MarkCustomerBillingInformationAsDefault(customerId, billingInfoId));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task MarkCustomerBillingInformationAsDefault_Should_Mark_The_BillingInfo_As_Default()
        {
            Customer customer = Company.Register("company", "1234567890");
            customer.AddBillingInformation("full name", "address", "city", "12345", "province", "italy", "", "1234567890", false);

            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Customer>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(customer));

            var repository = repositoryMock.Object;
            var authClient = new Mock<IAuthClient>().Object;

            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = customer.Id;
            Guid billingInfoId = customer.BillingInformation.First().Id;

            await commands.MarkCustomerBillingInformationAsDefault(customerId, billingInfoId);

            var billingInfo = customer.BillingInformation.FirstOrDefault(b => b.Id == billingInfoId);

            Assert.NotNull(billingInfo);
            Assert.True(billingInfo.IsDefault);
        }
        #endregion

        #region MarkCustomerShippingAddressAsDefault tests
        [Fact]
        public async Task MarkCustomerShippingAddressAsDefault_Should_Throw_ArgumentException_If_CustomerId_Is_Empty()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.Empty;
            Guid addressId = Guid.NewGuid();

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.MarkCustomerShippingAddressAsDefault(customerId, addressId));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task MarkCustomerShippingAddressAsDefault_Should_Throw_ArgumentException_If_AddressId_Is_Empty()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            Guid addressId = Guid.Empty;

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.MarkCustomerShippingAddressAsDefault(customerId, addressId));
            Assert.Equal(nameof(addressId), ex.ParamName);
        }

        [Fact]
        public async Task MarkCustomerShippingAddressAsDefault_Should_Throw_ArgumentOutOfRangeException_If_Customer_Is_Not_Found()
        {
            var repository = new Mock<IRepository>().Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = Guid.NewGuid();
            Guid addressId = Guid.NewGuid();

            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => commands.MarkCustomerShippingAddressAsDefault(customerId, addressId));
            Assert.Equal(nameof(customerId), ex.ParamName);
        }

        [Fact]
        public async Task MarkCustomerShippingAddressAsDefault_Should_Mark_Shipping_Address_As_Default()
        {
            Customer customer = Company.Register("company", "1234567890");
            customer.AddShippingAddress("address", "city", "12345", "province", "italy", false);

            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Customer>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(customer));

            var repository = repositoryMock.Object;
            var authClient = new Mock<IAuthClient>().Object;
            var commands = new CustomerCommands(repository, authClient);

            Guid customerId = customer.Id;
            Guid addressId = customer.ShippingAddresses.First().Id;

            await commands.MarkCustomerShippingAddressAsDefault(customerId, addressId);

            var shippingAddress = customer.ShippingAddresses.FirstOrDefault(s => s.Id == addressId);

            Assert.NotNull(shippingAddress);
            Assert.True(shippingAddress.IsDefault);
        }
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
