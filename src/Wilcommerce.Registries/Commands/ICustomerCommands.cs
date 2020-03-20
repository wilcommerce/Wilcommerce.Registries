using System;
using System.Threading.Tasks;
using Wilcommerce.Registries.Models;

namespace Wilcommerce.Registries.Commands
{
    /// <summary>
    /// Defines all the commands related to the Customer aggregate
    /// </summary>
    public interface ICustomerCommands
    {
        #region Person commands
        /// <summary>
        /// Register a new person
        /// </summary>
        /// <param name="firstName">The first name</param>
        /// <param name="lastName">The last name</param>
        /// <param name="nationalIdentificationNumber">The national identification number</param>
        /// <param name="gender">The gender</param>
        /// <param name="birthDate">The birth date</param>
        /// <returns></returns>
        Task RegisterNewPerson(string firstName, string lastName, string nationalIdentificationNumber, Gender gender, DateTime birthDate);

        /// <summary>
        /// Register a new person and the related account
        /// </summary>
        /// <param name="firstName">The first name</param>
        /// <param name="lastName">The last name</param>
        /// <param name="nationalIdentificationNumber">The national identification number</param>
        /// <param name="gender">The gender</param>
        /// <param name="birthDate">The birth date</param>
        /// <param name="userName">The username</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        Task RegisterNewPersonWithAccount(string firstName, string lastName, string nationalIdentificationNumber, Gender gender, DateTime birthDate, string userName, string password);

        /// <summary>
        /// Change the person general information
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="firstName">The first name</param>
        /// <param name="lastName">The last name</param>
        /// <param name="nationalIdentificationNumber">The national identification number</param>
        /// <param name="gender">The gender</param>
        /// <param name="birthDate">The birth date</param>
        /// <returns></returns>
        Task ChangePersonInfo(Guid customerId, string firstName, string lastName, string nationalIdentificationNumber, Gender gender, DateTime birthDate);
        #endregion

        #region Company commands
        /// <summary>
        /// Register a new company
        /// </summary>
        /// <param name="companyName">The company name</param>
        /// <param name="vatNumber">The company vat number</param>
        /// <param name="nationalIdentificationNumber">The company national identification number</param>
        /// <returns></returns>
        Task RegisterNewCompany(string companyName, string vatNumber, string nationalIdentificationNumber);

        /// <summary>
        /// Register a new company with the related account
        /// </summary>
        /// <param name="companyName">The company name</param>
        /// <param name="vatNumber">The company vat number</param>
        /// <param name="nationalIdentificationNumber">The company national identification number</param>
        /// <param name="userName">The username</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        Task RegisterNewCompanyWithAccount(string companyName, string vatNumber, string nationalIdentificationNumber, string userName, string password);

        /// <summary>
        /// Change the company general info
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="companyName">The company name</param>
        /// <param name="vatNumber">The company vat number</param>
        /// <param name="nationalIdentificationNumber">The company national identification number</param>
        /// <returns></returns>
        Task ChangeCompanyInfo(Guid customerId, string companyName, string vatNumber, string nationalIdentificationNumber);

        /// <summary>
        /// Change the company legal address
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="address">The address</param>
        /// <param name="city">The city</param>
        /// <param name="postalCode">The postal code</param>
        /// <param name="province">The province</param>
        /// <param name="country">The country</param>
        /// <returns></returns>
        Task ChangeCompanyLegalAddress(Guid customerId, string address, string city, string postalCode, string province, string country);
        #endregion

        #region Customer commands
        /// <summary>
        /// Delete the customer
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <returns></returns>
        Task DeleteCustomer(Guid customerId);

        /// <summary>
        /// Restore the customer
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <returns></returns>
        Task RestoreCustomer(Guid customerId);

        /// <summary>
        /// Lock the customer account
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <returns></returns>
        Task LockCustomerAccount(Guid customerId);

        /// <summary>
        /// Remove the customer account
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <returns></returns>
        Task RemoveCustomerAccount(Guid customerId);

        /// <summary>
        /// Set the customer account information
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="userName">The username</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        Task SetCustomerAccount(Guid customerId, string userName, string password);

        /// <summary>
        /// Add a shipping address
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="address">The address</param>
        /// <param name="city">The city</param>
        /// <param name="postalCode">The postal code</param>
        /// <param name="province">The province</param>
        /// <param name="country">The country</param>
        /// <param name="isDefault">Whether the shipping address is the default address</param>
        /// <returns></returns>
        Task AddCustomerShippingAddress(Guid customerId, string address, string city, string postalCode, string province, string country, bool isDefault);

        /// <summary>
        /// Change the shipping address information
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
        Task ChangeCustomerShippingAddress(Guid customerId, Guid addressId, string address, string city, string postalCode, string province, string country, bool isDefault);

        /// <summary>
        /// Remove the customer shipping address
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="addressId">The address id</param>
        /// <returns></returns>
        Task RemoveCustomerShippingAddress(Guid customerId, Guid addressId);

        /// <summary>
        /// Mark the shipping address as the default address
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="addressId">The address id</param>
        /// <returns></returns>
        Task MarkCustomerShippingAddressAsDefault(Guid customerId, Guid addressId);

        /// <summary>
        /// Add the billing information
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
        Task AddCustomerBillingInformation(Guid customerId, string fullName, string address, string city, string postalCode, string province, string country, string nationalIdentificationNumber, string vatNumber, bool isDefault);

        /// <summary>
        /// Change the billing information
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
        Task ChangeCustomerBillingInformation(Guid customerId, Guid billingInfoId, string fullName, string address, string city, string postalCode, string province, string country, string nationalIdentificationNumber, string vatNumber, bool isDefault);

        /// <summary>
        /// Remove the billing information
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="billingInfoId">The billing info id</param>
        /// <returns></returns>
        Task RemoveCustomerBillingInformation(Guid customerId, Guid billingInfoId);

        /// <summary>
        /// Mark the billing information as default
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="billingInfoId">The billing info id</param>
        /// <returns></returns>
        Task MarkCustomerBillingInformationAsDefault(Guid customerId, Guid billingInfoId);
        #endregion
    }
}
