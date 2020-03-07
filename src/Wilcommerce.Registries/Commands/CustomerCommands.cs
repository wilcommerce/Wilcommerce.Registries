using System;
using System.Threading.Tasks;
using Wilcommerce.Registries.Models;
using Wilcommerce.Registries.Repository;

namespace Wilcommerce.Registries.Commands
{
    /// <summary>
    /// Implementation of <see cref="ICustomerCommands"/>
    /// </summary>
    public class CustomerCommands : ICustomerCommands
    {
        /// <summary>
        /// Get the repository
        /// </summary>
        public IRepository Repository { get; }

        /// <summary>
        /// Construct the customer commands
        /// </summary>
        /// <param name="repository">The repository instance</param>
        public CustomerCommands(IRepository repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.AddCustomerBillingInformation(Guid, string, string, string, string, string, string, string, string, bool)"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="fullName">The full name</param>
        /// <param name="address">The address</param>
        /// <param name="city">The city</param>
        /// <param name="postalCode">The postal code</param>
        /// <param name="province">The province</param>
        /// <param name="country">The country</param>
        /// <param name="nationalIdentificationNumber">The national identification number</param>
        /// <param name="vatNumber">The vat number</param>
        /// <param name="isDefault">Whether the billing information is the default one</param>
        /// <returns></returns>
        public async Task AddCustomerBillingInformation(Guid customerId, string fullName, string address, string city, string postalCode, string province, string country, string nationalIdentificationNumber, string vatNumber, bool isDefault)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("value cannot be empty", nameof(fullName));
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("value cannot be empty", nameof(address));
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("value cannot be empty", nameof(city));
            }

            if (string.IsNullOrWhiteSpace(province))
            {
                throw new ArgumentException("value cannot be empty", nameof(province));
            }

            var customer = await Repository.GetByKeyAsync<Customer>(customerId);
            if (customer == null)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId));
            }

            customer.AddBillingInformation(fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault);
            await Repository.SaveChangesAsync();
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.AddCustomerShippingAddress(Guid, string, string, string, string, string, bool)"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="address">The address</param>
        /// <param name="city">The city</param>
        /// <param name="postalCode">The postal code</param>
        /// <param name="province">The province</param>
        /// <param name="country">The country</param>
        /// <param name="isDefault">Whether the shipping address is the default address</param>
        /// <returns></returns>
        public async Task AddCustomerShippingAddress(Guid customerId, string address, string city, string postalCode, string province, string country, bool isDefault)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("value cannot be empty", nameof(address));
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("value cannot be empty", nameof(city));
            }

            if (string.IsNullOrWhiteSpace(province))
            {
                throw new ArgumentException("value cannot be empty", nameof(province));
            }

            var customer = await Repository.GetByKeyAsync<Customer>(customerId);
            if (customer == null)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId));
            }

            customer.AddShippingAddress(address, city, postalCode, province, country, isDefault);
            await Repository.SaveChangesAsync();
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.ChangeCompanyInfo(Guid, string, string, string)"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="companyName">The company name</param>
        /// <param name="vatNumber">The company vat number</param>
        /// <param name="nationalIdentificationNumber">The company national identification number</param>
        /// <returns></returns>
        public async Task ChangeCompanyInfo(Guid customerId, string companyName, string vatNumber, string nationalIdentificationNumber)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            if (string.IsNullOrWhiteSpace(companyName))
            {
                throw new ArgumentException("value cannot be empty", nameof(companyName));
            }

            if (string.IsNullOrWhiteSpace(vatNumber))
            {
                throw new ArgumentException("value cannot be empty", nameof(vatNumber));
            }

            var customer = await Repository.GetByKeyAsync<Company>(customerId);
            if (customer == null)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId));
            }

            if (customer.CompanyName != companyName)
            {
                customer.ChangeCompanyName(companyName);
            }
            if (customer.VatNumber != vatNumber)
            {
                customer.ChangeVatNumber(vatNumber);
            }
            if (customer.NationalIdentificationNumber != nationalIdentificationNumber)
            {
                customer.SetNationalIdentificationNumber(nationalIdentificationNumber);
            }

            await Repository.SaveChangesAsync();
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.ChangeCompanyLegalAddress(Guid, string, string, string, string, string)"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="address">The address</param>
        /// <param name="city">The city</param>
        /// <param name="postalCode">The postal code</param>
        /// <param name="province">The province</param>
        /// <param name="country">The country</param>
        /// <returns></returns>
        public async Task ChangeCompanyLegalAddress(Guid customerId, string address, string city, string postalCode, string province, string country)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("value cannot be empty", nameof(address));
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("value cannot be empty", nameof(city));
            }

            if (string.IsNullOrWhiteSpace(province))
            {
                throw new ArgumentException("value cannot be empty", nameof(province));
            }

            var customer = await Repository.GetByKeyAsync<Company>(customerId);
            if (customer == null)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId));
            }

            customer.ChangeLegalAddress(address, city, postalCode, province, country);
            await Repository.SaveChangesAsync();
        }

        public Task ChangeCustomerBillingInformation(Guid customerId, Guid billingInfoId, string fullName, string address, string city, string postalCode, string province, string country, string nationalIdentificationNumber, string vatNumber, bool isDefault)
        {
            throw new NotImplementedException();
        }

        public Task ChangeCustomerShippingAddress(Guid customerId, Guid addressId, string address, string city, string postalCode, string province, string country, bool isDefault)
        {
            throw new NotImplementedException();
        }

        public Task ChangePersonInfo(Guid customerId, string firstName, string lastName, string nationalIdentificationNumber, Gender gender, DateTime birthDate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Task LockCustomerAccount(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Task MarkCustomerBillingInformationAsDefault(Guid customerId, Guid billingInfoId)
        {
            throw new NotImplementedException();
        }

        public Task MarkCustomerShippingAddressAsDefault(Guid customerId, Guid addressId)
        {
            throw new NotImplementedException();
        }

        public Task RegisterNewCompany(string companyName, string vatNumber, string nationalIdentificationNumber)
        {
            throw new NotImplementedException();
        }

        public Task RegisterNewCompany(string companyName, string vatNumber, string nationalIdentificationNumber, string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task RegisterNewPerson(string firstName, string lastName, string nationalIdentificationNumber, Gender gender, DateTime birthDate)
        {
            throw new NotImplementedException();
        }

        public Task RegisterNewPerson(string firstName, string lastName, string nationalIdentificationNumber, Gender gender, DateTime birthDate, string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCustomerAccount(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCustomerBillingInformation(Guid customerId, Guid billingInfoId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCustomerShippingAddress(Guid customerId, Guid addressId)
        {
            throw new NotImplementedException();
        }

        public Task RestoreCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Task SetCustomerAccount(Guid customerId, string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
