using System;
using System.Threading.Tasks;
using Wilcommerce.Core.Infrastructure;
using Wilcommerce.Registries.Events.Customer;
using Wilcommerce.Registries.Models;
using Wilcommerce.Registries.Repository;
using Wilcommerce.Registries.Services;

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
        public Repository.IRepository Repository { get; }
        
        /// <summary>
        /// Get the auth client
        /// </summary>
        public IAuthClient AuthClient { get; }

        /// <summary>
        /// Get the event bus
        /// </summary>
        public IEventBus EventBus { get; }

        /// <summary>
        /// Construct the customer commands
        /// </summary>
        /// <param name="repository">The repository instance</param>
        /// <param name="authClient">The auth client instance</param>
        /// <param name="eventBus">The event bus instance</param>
        public CustomerCommands(Repository.IRepository repository, IAuthClient authClient, IEventBus eventBus)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            AuthClient = authClient ?? throw new ArgumentNullException(nameof(authClient));
            EventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
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

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.ChangeCustomerBillingInformation(Guid, Guid, string, string, string, string, string, string, string, string, bool)"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="billingInfoId">The billing info id</param>
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
        public async Task ChangeCustomerBillingInformation(Guid customerId, Guid billingInfoId, string fullName, string address, string city, string postalCode, string province, string country, string nationalIdentificationNumber, string vatNumber, bool isDefault)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            if (billingInfoId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(billingInfoId));
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

            customer.ChangeBillingInfo(billingInfoId, fullName, address, city, postalCode, province, country, nationalIdentificationNumber, vatNumber, isDefault);
            await Repository.SaveChangesAsync();
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.ChangeCustomerShippingAddress(Guid, Guid, string, string, string, string, string, bool)"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="addressId">The address id</param>
        /// <param name="address">The address</param>
        /// <param name="city">The city</param>
        /// <param name="postalCode">The postal code</param>
        /// <param name="province">The province</param>
        /// <param name="country">The country</param>
        /// <param name="isDefault">Whether the shipping address is the default address</param>
        /// <returns></returns>
        public async Task ChangeCustomerShippingAddress(Guid customerId, Guid addressId, string address, string city, string postalCode, string province, string country, bool isDefault)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            if (addressId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(addressId));
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

            customer.ChangeShippingAddress(addressId, address, city, postalCode, province, country, isDefault);
            await Repository.SaveChangesAsync();
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.ChangePersonInfo(Guid, string, string, string, Gender, DateTime)"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="firstName">The first name</param>
        /// <param name="lastName">The last name</param>
        /// <param name="nationalIdentificationNumber">The national identification number</param>
        /// <param name="gender">The gender</param>
        /// <param name="birthDate">The birth date</param>
        /// <returns></returns>
        public async Task ChangePersonInfo(Guid customerId, string firstName, string lastName, string nationalIdentificationNumber, Gender gender, DateTime birthDate)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("value cannot be empty", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("value cannot be empty", nameof(lastName));
            }

            var customer = await Repository.GetByKeyAsync<Person>(customerId);
            if (customer == null)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId));
            }

            if (firstName != customer.FirstName)
            {
                customer.ChangeFirstName(firstName);
            }
            if (lastName != customer.LastName)
            {
                customer.ChangeLastName(lastName);
            }
            if (nationalIdentificationNumber != customer.NationalIdentificationNumber)
            {
                customer.SetNationalIdentificationNumber(nationalIdentificationNumber);
            }
            if (gender != customer.Gender)
            {
                customer.ChangeGender(gender);
            }
            if (birthDate != customer.BirthDate)
            {
                customer.ChangeBirthDate(birthDate);
            }

            await Repository.SaveChangesAsync();
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.DeleteCustomer(Guid)"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <returns></returns>
        public async Task DeleteCustomer(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            var customer = await Repository.GetByKeyAsync<Customer>(customerId);
            if (customer == null)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId));
            }

            try
            {
                customer.Delete();
                if (customer.HasAccount)
                {
                    await AuthClient.DisableAccount(customer.Account.UserId);
                    customer.LockAccount();
                }

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.LockCustomerAccount(Guid)"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <returns></returns>
        public async Task LockCustomerAccount(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            var customer = await Repository.GetByKeyAsync<Customer>(customerId);
            if (customer == null)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId));
            }

            try
            {
                customer.LockAccount();
                await AuthClient.DisableAccount(customer.Account.UserId);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.MarkCustomerBillingInformationAsDefault(Guid, Guid)"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="billingInfoId">The billing info id</param>
        /// <returns></returns>
        public async Task MarkCustomerBillingInformationAsDefault(Guid customerId, Guid billingInfoId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            if (billingInfoId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(billingInfoId));
            }

            var customer = await Repository.GetByKeyAsync<Customer>(customerId);
            if (customer == null)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId));
            }

            customer.MarkBillingInfoAsDefault(billingInfoId);
            await Repository.SaveChangesAsync();
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.MarkCustomerShippingAddressAsDefault(Guid, Guid)"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="addressId">The address id</param>
        /// <returns></returns>
        public async Task MarkCustomerShippingAddressAsDefault(Guid customerId, Guid addressId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            if (addressId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(addressId));
            }

            var customer = await Repository.GetByKeyAsync<Customer>(customerId);
            if (customer == null)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId));
            }

            customer.MarkShippingAddressAsDefault(addressId);
            await Repository.SaveChangesAsync();
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.RegisterNewCompany(string, string, string)"/>
        /// </summary>
        /// <param name="companyName">The company name</param>
        /// <param name="vatNumber">The company vat number</param>
        /// <param name="nationalIdentificationNumber">The company national identification number</param>
        /// <returns></returns>
        public async Task RegisterNewCompany(string companyName, string vatNumber, string nationalIdentificationNumber)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                throw new ArgumentException("value cannot be empty", nameof(companyName));
            }

            if (string.IsNullOrWhiteSpace(vatNumber))
            {
                throw new ArgumentException("value cannot be empty", nameof(vatNumber));
            }

            var company = Company.Register(companyName, vatNumber);
            if (!string.IsNullOrWhiteSpace(nationalIdentificationNumber))
            {
                company.SetNationalIdentificationNumber(nationalIdentificationNumber);
            }

            Repository.Add(company);
            await Repository.SaveChangesAsync();
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.RegisterNewCompanyWithAccount(string, string, string, string, string)"/>
        /// </summary>
        /// <param name="companyName">The company name</param>
        /// <param name="vatNumber">The company vat number</param>
        /// <param name="nationalIdentificationNumber">The company national identification number</param>
        /// <param name="userName">The username</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        public async Task RegisterNewCompanyWithAccount(string companyName, string vatNumber, string nationalIdentificationNumber, string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                throw new ArgumentException("value cannot be empty", nameof(companyName));
            }

            if (string.IsNullOrWhiteSpace(vatNumber))
            {
                throw new ArgumentException("value cannot be empty", nameof(vatNumber));
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("value cannot be empty", nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("value cannot be empty", nameof(password));
            }

            try
            {
                var userId = await AuthClient.RegisterNewAccount(userName, password);
                var company = Company.RegisterWithAccount(companyName, vatNumber, userId, userName);
                if (!string.IsNullOrWhiteSpace(nationalIdentificationNumber))
                {
                    company.SetNationalIdentificationNumber(nationalIdentificationNumber);
                }

                Repository.Add(company);
                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.RegisterNewPerson(string, string, string, Gender, DateTime)"/>
        /// </summary>
        /// <param name="firstName">The first name</param>
        /// <param name="lastName">The last name</param>
        /// <param name="nationalIdentificationNumber">The national identification number</param>
        /// <param name="gender">The gender</param>
        /// <param name="birthDate">The birth date</param>
        /// <returns></returns>
        public async Task RegisterNewPerson(string firstName, string lastName, string nationalIdentificationNumber, Gender gender, DateTime birthDate)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("value cannot be empty", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("value cannot be empty", nameof(lastName));
            }

            if (birthDate >= DateTime.Today)
            {
                throw new ArgumentException("Birth date cannot be after today", nameof(birthDate));
            }

            var person = Person.Register(firstName, lastName, gender, birthDate);
            if (!string.IsNullOrWhiteSpace(nationalIdentificationNumber))
            {
                person.SetNationalIdentificationNumber(nationalIdentificationNumber);
            }

            Repository.Add(person);
            await Repository.SaveChangesAsync();

            var @event = new PersonRegisteredEvent(person.Id, firstName, lastName, gender, birthDate);
            EventBus.RaiseEvent(@event);
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.RegisterNewPersonWithAccount(string, string, string, Gender, DateTime, string, string)"/>
        /// </summary>
        /// <param name="firstName">The first name</param>
        /// <param name="lastName">The last name</param>
        /// <param name="nationalIdentificationNumber">The national identification number</param>
        /// <param name="gender">The gender</param>
        /// <param name="birthDate">The birth date</param>
        /// <param name="userName">The username</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        public async Task RegisterNewPersonWithAccount(string firstName, string lastName, string nationalIdentificationNumber, Gender gender, DateTime birthDate, string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("value cannot be empty", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("value cannot be empty", nameof(lastName));
            }

            if (birthDate >= DateTime.Today)
            {
                throw new ArgumentException("Birth date cannot be after today", nameof(birthDate));
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("value cannot be empty", nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("value cannot be empty", nameof(password));
            }

            try
            {
                var userId = await AuthClient.RegisterNewAccount(userName, password);
                var person = Person.RegisterWithAccount(firstName, lastName, gender, birthDate, userId, userName);
                if (!string.IsNullOrWhiteSpace(nationalIdentificationNumber))
                {
                    person.SetNationalIdentificationNumber(nationalIdentificationNumber);
                }

                Repository.Add(person);
                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.RemoveCustomerAccount(Guid)"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <returns></returns>
        public async Task RemoveCustomerAccount(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            var customer = await Repository.GetByKeyAsync<Customer>(customerId);
            if (customer == null)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId));
            }

            try
            {
                var accountId = customer.Account.UserId;

                customer.RemoveAccount();
                await AuthClient.DisableAccount(accountId);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.RemoveCustomerBillingInformation(Guid, Guid)"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="billingInfoId">The billing info id</param>
        /// <returns></returns>
        public async Task RemoveCustomerBillingInformation(Guid customerId, Guid billingInfoId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            if (billingInfoId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(billingInfoId));
            }

            var customer = await Repository.GetByKeyAsync<Customer>(customerId);
            if (customer == null)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId));
            }

            customer.RemoveBillingInfo(billingInfoId);
            await Repository.SaveChangesAsync();
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.RemoveCustomerShippingAddress(Guid, Guid)"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="addressId">The address id</param>
        /// <returns></returns>
        public async Task RemoveCustomerShippingAddress(Guid customerId, Guid addressId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            if (addressId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(addressId));
            }

            var customer = await Repository.GetByKeyAsync<Customer>(customerId);
            if (customer == null)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId));
            }

            customer.RemoveShippingAddress(addressId);
            await Repository.SaveChangesAsync();
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.RestoreCustomer(Guid)"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <returns></returns>
        public async Task RestoreCustomer(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            var customer = await Repository.GetByKeyAsync<Customer>(customerId);
            if (customer == null)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId));
            }

            try
            {
                customer.Restore();
                if (customer.HasAccount && customer.Account.IsLocked)
                {
                    await AuthClient.EnableAccount(customer.Account.UserId);
                    customer.UnlockAccount();
                }

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICustomerCommands.SetCustomerAccount(Guid, string, string)"/>
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="userName">The username</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        public async Task SetCustomerAccount(Guid customerId, string userName, string password)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(customerId));
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("value cannot be empty", nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("value cannot be empty", nameof(password));
            }

            var customer = await Repository.GetByKeyAsync<Customer>(customerId);
            if (customer == null)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId));
            }

            try
            {
                var userId = await AuthClient.FindOrRegisterAccount(userName, password);
                customer.SetAccount(userId, userName);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }
    }
}
