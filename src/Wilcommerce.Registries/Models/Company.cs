using System;

namespace Wilcommerce.Registries.Models
{
    /// <summary>
    /// Represent a customer of type company
    /// </summary>
    public class Company : Customer
    {
        #region Constructor
        /// <summary>
        /// Construct the customer as a company
        /// </summary>
        protected Company() : base()
        {
            LegalAddress = new PostalAddress();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the company name
        /// </summary>
        public string CompanyName { get; protected set; }

        /// <summary>
        /// Get or set the company vat number
        /// </summary>
        public string VatNumber { get; protected set; }

        /// <summary>
        /// Get or set the legal address of the company
        /// </summary>
        public virtual PostalAddress LegalAddress { get; protected set; }
        #endregion

        #region Behaviors
        /// <summary>
        /// Change the company name
        /// </summary>
        /// <param name="companyName">The company name</param>
        public virtual void ChangeCompanyName(string companyName)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                throw new ArgumentException("value cannot be empty", nameof(companyName));
            }

            CompanyName = companyName;
        }

        /// <summary>
        /// Change the company vat number
        /// </summary>
        /// <param name="vatNumber">The vat number</param>
        public virtual void ChangeVatNumber(string vatNumber)
        {
            if (string.IsNullOrWhiteSpace(vatNumber))
            {
                throw new ArgumentException("value cannot be empty", nameof(vatNumber));
            }

            VatNumber = vatNumber;
        }

        /// <summary>
        /// Change the company legal address
        /// </summary>
        /// <param name="address">The address</param>
        /// <param name="city">The city</param>
        /// <param name="postalCode">The postal code</param>
        /// <param name="province">The province</param>
        /// <param name="country">The country</param>
        public virtual void ChangeLegalAddress(string address, string city, string postalCode, string province, string country)
        {
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

            LegalAddress = new PostalAddress
            {
                Address = address,
                City = city,
                Country = country,
                PostalCode = postalCode,
                Province = province
            };
        }
        #endregion

        #region Factory methods
        /// <summary>
        /// Create a new company customer
        /// </summary>
        /// <param name="companyName">The company name</param>
        /// <param name="vatNumber">The company vat number</param>
        /// <returns>The new customer created</returns>
        public static Company Register(string companyName, string vatNumber)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                throw new ArgumentException("value cannot be empty", nameof(companyName));
            }

            if (string.IsNullOrWhiteSpace(vatNumber))
            {
                throw new ArgumentException("value cannot be empty", nameof(vatNumber));
            }

            var customer = new Company
            {
                Id = Guid.NewGuid(),
                CompanyName = companyName,
                VatNumber = vatNumber,
                CreationDate = DateTime.Now
            };

            return customer;
        }

        /// <summary>
        /// Create a new company customer with an account associated
        /// </summary>
        /// <param name="companyName">The company name</param>
        /// <param name="vatNumber">The company vat number</param>
        /// <param name="userId">The customer's user id</param>
        /// <param name="userName">The customer's username</param>
        /// <returns>The new customer created</returns>
        public static Company RegisterWithAccount(string companyName, string vatNumber, Guid userId, string userName)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                throw new ArgumentException("value cannot be empty", nameof(companyName));
            }

            if (string.IsNullOrWhiteSpace(vatNumber))
            {
                throw new ArgumentException("value cannot be empty", nameof(vatNumber));
            }

            if (userId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("value cannot be empty", nameof(userName));
            }

            var customer = new Company
            {
                Id = Guid.NewGuid(),
                CompanyName = companyName,
                VatNumber = vatNumber,
                Account = new AccountInfo { UserId = userId, UserName = userName },
                CreationDate = DateTime.Now
            };

            return customer;
        }
        #endregion
    }
}
