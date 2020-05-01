using System;

namespace Wilcommerce.Registries.Models
{
    /// <summary>
    /// Represent a customer as a person
    /// </summary>
    public class Person : Customer
    {
        #region Constructor
        /// <summary>
        /// Construct the customer as a person
        /// </summary>
        protected Person() : base()
        {
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
        /// Get or set the customer's date of birth
        /// </summary>
        public DateTime? BirthDate { get; protected set; }

        /// <summary>
        /// Get or set the customer's gender
        /// </summary>
        public Gender? Gender { get; protected set; }
        #endregion

        #region Behaviors
        /// <summary>
        /// Change the person's first name
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
        /// Change the person's last name
        /// </summary>
        /// <param name="lastName">The last name</param>
        public virtual void ChangeLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("value cannot be empty", nameof(lastName));
            }

            LastName = lastName;
        }

        /// <summary>
        /// Change the person's birth date
        /// </summary>
        /// <param name="birthDate">The birth date</param>
        public virtual void ChangeBirthDate(DateTime birthDate)
        {
            if (birthDate >= DateTime.Today)
            {
                throw new ArgumentException("Birth date cannot be greater than or equal to today", nameof(birthDate));
            }

            BirthDate = birthDate;
        }

        /// <summary>
        /// Change the person's gender
        /// </summary>
        /// <param name="gender">The gender</param>
        public virtual void ChangeGender(Gender gender)
        {
            Gender = gender;
        }
        #endregion

        #region Factory Methods
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
        public static Person RegisterWithAccount(string firstName, string lastName, Gender gender, DateTime birthDate, Guid userId, string userName)
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

            var customer = new Person
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
        public static Person Register(string firstName, string lastName, Gender gender, DateTime birthDate)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("value cannot be empty", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("value cannot be empty", nameof(lastName));
            }

            var customer = new Person
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
        #endregion
    }
}
