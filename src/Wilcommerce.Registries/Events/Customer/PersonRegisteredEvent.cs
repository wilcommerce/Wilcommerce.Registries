using System;
using Wilcommerce.Core.Infrastructure;
using Wilcommerce.Registries.Models;

namespace Wilcommerce.Registries.Events.Customer
{
    /// <summary>
    /// Person registered
    /// </summary>
    public class PersonRegisteredEvent : DomainEvent
    {
        /// <summary>
        /// Get the person id
        /// </summary>
        public Guid PersonId { get; private set; }
        
        /// <summary>
        /// Get the person first name
        /// </summary>
        public string FirstName { get; private set; }
        
        /// <summary>
        /// Get the person last name
        /// </summary>
        public string LastName { get; private set; }
        
        /// <summary>
        /// Get the person gender
        /// </summary>
        public Gender Gender { get; private set; }
        
        /// <summary>
        /// Get the person birth date
        /// </summary>
        public DateTime BirthDate { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="personId">The person id</param>
        /// <param name="firstName">The person first name</param>
        /// <param name="lastName">The person last name</param>
        /// <param name="gender">The person gender</param>
        /// <param name="birthDate">The person birth date</param>
        public PersonRegisteredEvent(Guid personId, string firstName, string lastName, Gender gender, DateTime birthDate) 
            : base(personId, typeof(Models.Customer))
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Person {FirstName} {LastName}, gender {Gender.ToString()}, birth date {BirthDate.ToShortDateString()} registered";
        }
    }
}
