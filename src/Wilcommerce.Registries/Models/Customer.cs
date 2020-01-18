using System;
using System.Collections.Generic;
using System.Text;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Registries.Models
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    public class Customer : IAggregateRoot
    {
        /// <summary>
        /// Get or set the customer's id
        /// </summary>
        public Guid Id { get; protected set; }

        #region Constructor
        /// <summary>
        /// Construct the customer
        /// </summary>
        protected Customer()
        {
            Account = AccountInfo.EmptyAccount();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the customer's first name
        /// </summary>
        public string FirstName { get; protected set; }

        /// <summary>
        /// Get or set the customer's last name
        /// </summary>
        public string LastName { get; protected set; }

        /// <summary>
        /// Get or set the company name
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Get or set the customer's date of birth
        /// </summary>
        public DateTime? BirthDate { get; protected set; }

        /// <summary>
        /// Get or set the customer's gender
        /// </summary>
        public Gender? Gender { get; protected set; }

        /// <summary>
        /// Get or set the account information
        /// </summary>
        public virtual AccountInfo Account { get; set; }

        /// <summary>
        /// Get or set whether the product is deleted
        /// </summary>
        public bool Deleted { get; protected set; }

        /// <summary>
        /// Get the creation date
        /// </summary>
        public DateTime CreationDate { get; protected set; }

        /// <summary>
        /// Get whether the custom as an account associated
        /// </summary>
        public bool HasAccount => Account != AccountInfo.EmptyAccount();
        #endregion

        #region Behaviors
        /// <summary>
        /// Delete the customer
        /// </summary>
        public virtual void Delete()
        {
            if (Deleted)
            {
                throw new InvalidOperationException("Customer already deleted");
            }

            Deleted = true;
        }

        /// <summary>
        /// Restore the customer
        /// </summary>
        public virtual void Restore()
        {
            if (!Deleted)
            {
                throw new InvalidOperationException("Customer is not deleted");
            }

            Deleted = false;
        }

        /// <summary>
        /// Change the customer's first name
        /// </summary>
        /// <param name="firstName">The first name</param>
        public virtual void ChangeFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("value cannot be empty", nameof(firstName));
            }

            FirstName = firstName;
        }

        /// <summary>
        /// Change the customer's last name
        /// </summary>
        /// <param name="lastName">The last name</param>
        public virtual void ChangeLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("value cannot be null", nameof(lastName));
            }

            LastName = lastName;
        }

        /// <summary>
        /// Change the customer's birth date
        /// </summary>
        /// <param name="birthDate">The birth date</param>
        public virtual void ChangeBirthDate(DateTime birthDate)
        {
            BirthDate = birthDate;
        }

        /// <summary>
        /// Change the customer's gender
        /// </summary>
        /// <param name="gender">The gender</param>
        public virtual void ChangeGender(Gender gender)
        {
            Gender = gender;
        }

        /// <summary>
        /// Set the customer's company name
        /// </summary>
        /// <param name="companyName">The company name</param>
        public virtual void SetCompanyName(string companyName)
        {
            CompanyName = companyName;
        }

        /// <summary>
        /// Set the account associated to the customer
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="userName">The username</param>
        public virtual void SetAccount(Guid userId, string userName)
        {
            Account = new AccountInfo { UserId = userId, UserName = userName };
        }

        /// <summary>
        /// Remove the account information from the customer
        /// </summary>
        public virtual void RemoveAccount()
        {
            Account = AccountInfo.EmptyAccount();
        }

        /// <summary>
        /// Lock the customer account
        /// </summary>
        public virtual void LockAccount()
        {
            if (!HasAccount)
            {
                throw new InvalidOperationException("Customer hasn't any account associated");
            }

            Account.IsLocked = true;
        }

        /// <summary>
        /// Unlock the customer account
        /// </summary>
        public virtual void UnlockAccount()
        {
            if (!HasAccount)
            {
                throw new InvalidOperationException("Customer hasn't any account associated");
            }
            if (!Account.IsLocked)
            {
                throw new InvalidOperationException("Customer account is not locked");
            }

            Account.IsLocked = false;
        }
        #endregion

        #region Factory methods
        /// <summary>
        /// Create a new customer with an account associated
        /// </summary>
        /// <param name="firstName">The customer's first name</param>
        /// <param name="lastName">The customer's last name</param>
        /// <param name="gender">The customer's gender</param>
        /// <param name="birthDate">The customer's birth date</param>
        /// <param name="userId">The customer's user id</param>
        /// <param name="userName">The customer's user name</param>
        /// <returns>The new customer created</returns>
        public static Customer CreateWithAccount(string firstName, string lastName, Gender gender, DateTime birthDate, Guid userId, string userName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("value cannot be empty", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("value cannot be empty", nameof(lastName));
            }

            if (userId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("value cannot be empty", nameof(userName));
            }

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                BirthDate = birthDate,
                Account = new AccountInfo { UserId = userId, UserName = userName },
                CreationDate = DateTime.Now
            };

            return customer;
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <param name="firstName">The customer's first name</param>
        /// <param name="lastName">The customer's last name</param>
        /// <param name="gender">The customer's gender</param>
        /// <param name="birthDate">The customer's birth date</param>
        /// <returns>The new customer created</returns>
        public static Customer Create(string firstName, string lastName, Gender gender, DateTime birthDate)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("value cannot be empty", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("value cannot be empty", nameof(lastName));
            }

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                BirthDate = birthDate,
                CreationDate = DateTime.Now
            };

            return customer;
        }

        /// <summary>
        /// Create a new company customer
        /// </summary>
        /// <param name="companyName">The company name</param>
        /// <returns>The new customer created</returns>
        public static Customer CreateAsCompany(string companyName)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                throw new ArgumentException("value cannot be empty", nameof(companyName));
            }

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                CompanyName = companyName,
                CreationDate = DateTime.Now
            };

            return customer;
        }

        /// <summary>
        /// Create a new company customer with an account associated
        /// </summary>
        /// <param name="companyName">The company name</param>
        /// <param name="userId">The customer's user id</param>
        /// <param name="userName">The customer's username</param>
        /// <returns>The new customer created</returns>
        public static Customer CreateAsCompanyWithAccount(string companyName, Guid userId, string userName)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                throw new ArgumentException("value cannot be empty", nameof(companyName));
            }

            if (userId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(userId));
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("value cannot be empty", nameof(userName));
            }

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                CompanyName = companyName,
                Account = new AccountInfo { UserId = userId, UserName = userName },
                CreationDate = DateTime.Now
            };

            return customer;
        }
        #endregion
    }
}
