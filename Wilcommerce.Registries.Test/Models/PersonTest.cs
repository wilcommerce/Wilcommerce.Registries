using System;
using Wilcommerce.Registries.Models;
using Xunit;

namespace Wilcommerce.Registries.Test.Models
{
    public class PersonTest
    {
        #region RegisterWithAccount Factory test
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void RegisterWithAccount_Should_Throw_ArgumentException_If_First_Name_Is_Empty(string firstName)
        {
            string lastName = "Lastname";
            Gender gender = Gender.Female;
            DateTime birthDate = new DateTime(1987, 1, 1);
            Guid userId = Guid.NewGuid();
            string userName = "username";

            var ex = Assert.Throws<ArgumentException>(() => Person.RegisterWithAccount(firstName, lastName, gender, birthDate, userId, userName));
            Assert.Equal(nameof(firstName), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void RegisterWithAccount_Should_Throw_ArgumentException_If_Last_Name_Is_Empty(string lastName)
        {
            string firstName = "Firstname";
            Gender gender = Gender.Female;
            DateTime birthDate = new DateTime(1987, 1, 1);
            Guid userId = Guid.NewGuid();
            string userName = "username";

            var ex = Assert.Throws<ArgumentException>(() => Person.RegisterWithAccount(firstName, lastName, gender, birthDate, userId, userName));
            Assert.Equal(nameof(lastName), ex.ParamName);
        }

        [Fact]
        public void RegisterWithAccount_Should_Throw_ArgumentException_If_UserId_Is_Empty()
        {
            string firstName = "Firstname";
            string lastName = "Lastname";
            Gender gender = Gender.Female;
            DateTime birthDate = new DateTime(1987, 1, 1);
            Guid userId = Guid.Empty;
            string userName = "username";

            var ex = Assert.Throws<ArgumentException>(() => Person.RegisterWithAccount(firstName, lastName, gender, birthDate, userId, userName));
            Assert.Equal(nameof(userId), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void RegisterWithAccount_Should_Throw_ArgumentException_If_User_Name_Is_Empty(string userName)
        {
            string firstName = "Firstname";
            string lastName = "Lastname";
            Gender gender = Gender.Female;
            DateTime birthDate = new DateTime(1987, 1, 1);
            Guid userId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentException>(() => Person.RegisterWithAccount(firstName, lastName, gender, birthDate, userId, userName));
            Assert.Equal(nameof(userName), ex.ParamName);
        }

        [Fact]
        public void RegisterWithAccount_Should_Create_The_Customer_With_The_Specified_Values()
        {
            string firstName = "Firstname";
            string lastName = "Lastname";
            Gender gender = Gender.Female;
            DateTime birthDate = new DateTime(1987, 1, 1);
            Guid userId = Guid.NewGuid();
            string userName = "username";

            var person = Person.RegisterWithAccount(firstName, lastName, gender, birthDate, userId, userName);

            Assert.Equal(firstName, person.FirstName);
            Assert.Equal(lastName, person.LastName);
            Assert.Equal(gender, person.Gender);
            Assert.Equal(birthDate, person.BirthDate);
            Assert.Equal(userId, person.Account?.UserId);
            Assert.Equal(userName, person.Account?.UserName);
        }
        #endregion

        #region Register Factory test
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Register_Should_Throw_ArgumentException_If_First_Name_Is_Empty(string firstName)
        {
            string lastName = "Lastname";
            Gender gender = Gender.Female;
            DateTime birthDate = new DateTime(1987, 1, 1);

            var ex = Assert.Throws<ArgumentException>(() => Person.Register(firstName, lastName, gender, birthDate));
            Assert.Equal(nameof(firstName), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Register_Should_Throw_ArgumentException_If_Last_Name_Is_Empty(string lastName)
        {
            string firstName = "Firstname";
            Gender gender = Gender.Female;
            DateTime birthDate = new DateTime(1987, 1, 1);

            var ex = Assert.Throws<ArgumentException>(() => Person.Register(firstName, lastName, gender, birthDate));
            Assert.Equal(nameof(lastName), ex.ParamName);
        }

        [Fact]
        public void Register_Should_Create_The_Customer_With_The_Specified_Values()
        {
            string firstName = "Firstname";
            string lastName = "Lastname";
            Gender gender = Gender.Female;
            DateTime birthDate = new DateTime(1987, 1, 1);

            var person = Person.Register(firstName, lastName, gender, birthDate);

            Assert.Equal(firstName, person.FirstName);
            Assert.Equal(lastName, person.LastName);
            Assert.Equal(gender, person.Gender);
            Assert.Equal(birthDate, person.BirthDate);
        }
        #endregion

        #region ChangeFirstName test
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ChangeFirstName_Should_Throw_ArgumentException_If_First_Name_Is_Empty(string firstName)
        {
            var person = Person.Register("Name", "Lastname", Gender.Male, new DateTime(1987, 1, 1));

            var ex = Assert.Throws<ArgumentException>(() => person.ChangeFirstName(firstName));
            Assert.Equal(nameof(firstName), ex.ParamName);
        }

        [Fact]
        public void ChangeFirstName_Should_Change_The_First_Name_With_The_Specified_Value()
        {
            var person = Person.Register("Name", "Lastname", Gender.Male, new DateTime(1987, 1, 1));

            string firstName = "Firstname";
            person.ChangeFirstName(firstName);

            Assert.Equal(firstName, person.FirstName);
        }
        #endregion

        #region ChangeLastName test
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ChangeLastName_Should_Throw_ArgumentException_If_Last_Name_Is_Empty(string lastName)
        {
            var person = Person.Register("Name", "Lastname", Gender.Male, new DateTime(1987, 1, 1));

            var ex = Assert.Throws<ArgumentException>(() => person.ChangeLastName(lastName));
            Assert.Equal(nameof(lastName), ex.ParamName);
        }

        [Fact]
        public void ChangeLastName_Should_Change_The_Last_Name_With_The_Specified_Value()
        {
            var person = Person.Register("Name", "Lastname", Gender.Male, new DateTime(1987, 1, 1));

            string lastName = "new lastname";
            person.ChangeLastName(lastName);

            Assert.Equal(lastName, person.LastName);
        }
        #endregion

        #region ChangeBirthDate test
        [Fact]
        public void ChangeBirthDate_Should_Throw_ArgumentException_If_Birth_Date_Is_Greater_Than_Or_Equal_To_Today()
        {
            var person = Person.Register("name", "lastname", Gender.Female, new DateTime(1987, 1, 1));

            var birthDate = DateTime.Today;

            var ex = Assert.Throws<ArgumentException>(() => person.ChangeBirthDate(birthDate));
            Assert.Equal(nameof(birthDate), ex.ParamName);
        }

        [Fact]
        public void ChangeBirthDate_Should_Change_The_Birth_Date_With_The_Specified_Value()
        {
            var person = Person.Register("name", "lastname", Gender.Female, new DateTime(1987, 1, 1));

            var birthDate = new DateTime(1987, 8, 24);
            person.ChangeBirthDate(birthDate);

            Assert.Equal(birthDate, person.BirthDate);
        }
        #endregion

        #region ChangeGender test
        [Fact]
        public void ChangeGender_Should_Change_The_Gender_With_The_Specified_Value()
        {
            var person = Person.Register("firstname", "lastname", Gender.Male, new DateTime(1987, 1, 1));

            var gender = Gender.Female;
            person.ChangeGender(gender);

            Assert.Equal(gender, person.Gender);
        }
        #endregion

        #region SetNationalIdentificationNumber test
        [Fact]
        public void SetNationalIdentificationNumber_Should_Set_The_National_Identification_Number_With_The_Specified_Value()
        {
            var person = Person.Register("name", "lastname", Gender.Female, new DateTime(1987, 1, 1));

            string nationalIdentificationNumber = "nationalidentificationnumber";
            person.SetNationalIdentificationNumber(nationalIdentificationNumber);

            Assert.Equal(nationalIdentificationNumber, person.NationalIdentificationNumber);
        }
        #endregion
    }
}
