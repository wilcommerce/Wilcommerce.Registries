using System;
using Wilcommerce.Registries.Events.Customer;
using Wilcommerce.Registries.Models;
using Xunit;

namespace Wilcommerce.Registries.Test.Events.Customer
{
    public class PersonRegisteredWithAccountEventTest
    {
        [Fact]
        public void Ctor_Should_Set_All_Values_Correctly()
        {
            Guid personId = Guid.NewGuid();
            string firstName = "firstname";
            string lastName = "lastname";
            Gender gender = Gender.Male;
            DateTime birthDate = new DateTime(1980, 1, 1);
            string userName = "username";

            var ev = new PersonRegisteredWithAccountEvent(personId, firstName, lastName, gender, birthDate, userName);

            Assert.Equal(personId, ev.PersonId);
            Assert.Equal(firstName, ev.FirstName);
            Assert.Equal(lastName, ev.LastName);
            Assert.Equal(gender, ev.Gender);
            Assert.Equal(birthDate, ev.BirthDate);
            Assert.Equal(userName, ev.UserName);

            Assert.Equal(personId, ev.AggregateId);
            Assert.Equal(typeof(Registries.Models.Customer), ev.AggregateType);
            Assert.Equal(DateTime.Today, ev.FiredOn.Date);
        }

        [Fact]
        public void ToString_Should_Return_Event_Formatted_As_String()
        {
            Guid personId = Guid.NewGuid();
            string firstName = "firstname";
            string lastName = "lastname";
            Gender gender = Gender.Male;
            DateTime birthDate = new DateTime(1980, 1, 1);
            string userName = "username";

            var ev = new PersonRegisteredWithAccountEvent(personId, firstName, lastName, gender, birthDate, userName);

            string eventAsString = $"Person {firstName} {lastName}, gender {gender.ToString()}, birth date {birthDate.ToShortDateString()} and username {userName} registered";

            Assert.Equal(eventAsString, ev.ToString());
        }
    }
}
