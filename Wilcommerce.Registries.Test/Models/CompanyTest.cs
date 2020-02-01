using System;
using Wilcommerce.Registries.Models;
using Xunit;

namespace Wilcommerce.Registries.Test.Models
{
    public class CompanyTest
    {
        #region Register Factory test
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Register_Should_Throw_ArgumentException_If_Company_Name_Is_Empty(string companyName)
        {
            string vatNumber = "1234567890";

            var ex = Assert.Throws<ArgumentException>(() => Company.Register(companyName, vatNumber));
            Assert.Equal(nameof(companyName), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Register_Should_Throw_ArgumentException_If_Vat_Number_Is_Empty(string vatNumber)
        {
            string companyName = "Company";

            var ex = Assert.Throws<ArgumentException>(() => Company.Register(companyName, vatNumber));
            Assert.Equal(nameof(vatNumber), ex.ParamName);
        }

        [Fact]
        public void Register_Should_Create_Customer_With_The_Specified_Values()
        {
            string companyName = "company";
            string vatNumber = "1234567890";

            var company = Company.Register(companyName, vatNumber);

            Assert.Equal(companyName, company.CompanyName);
            Assert.Equal(vatNumber, company.VatNumber);
        }
        #endregion

        #region RegisterWithAccount Factory test
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void RegisterWithAccount_Should_Throw_ArgumentException_If_Company_Name_Is_Empty(string companyName)
        {
            string vatNumber = "1234567890";
            Guid userId = Guid.NewGuid();
            string userName = "username";

            var ex = Assert.Throws<ArgumentException>(() => Company.RegisterWithAccount(companyName, vatNumber, userId, userName));
            Assert.Equal(nameof(companyName), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void RegisterWithAccount_Should_Throw_ArgumentException_If_Vat_Number_Is_Empty(string vatNumber)
        {
            string companyName = "company";
            Guid userId = Guid.NewGuid();
            string userName = "username";

            var ex = Assert.Throws<ArgumentException>(() => Company.RegisterWithAccount(companyName, vatNumber, userId, userName));
            Assert.Equal(nameof(vatNumber), ex.ParamName);
        }

        [Fact]
        public void RegisterWithAccount_Should_Throw_ArgumentException_If_UserId_Is_Empty()
        {
            string companyName = "company";
            string vatNumber = "1234567890";
            Guid userId = Guid.Empty;
            string userName = "username";

            var ex = Assert.Throws<ArgumentException>(() => Company.RegisterWithAccount(companyName, vatNumber, userId, userName));
            Assert.Equal(nameof(userId), ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void RegisterWithAccount_Should_Throw_ArgumentException_If_User_Name_Is_Empty(string userName)
        {
            string companyName = "company";
            string vatNumber = "1234567890";
            Guid userId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentException>(() => Company.RegisterWithAccount(companyName, vatNumber, userId, userName));
            Assert.Equal(nameof(userName), ex.ParamName);
        }

        [Fact]
        public void RegisterWithAccount_Should_Create_Customer_With_The_Specified_Values()
        {
            string companyName = "company";
            string vatNumber = "1234567890";
            Guid userId = Guid.NewGuid();
            string userName = "username";

            var company = Company.RegisterWithAccount(companyName, vatNumber, userId, userName);

            Assert.Equal(companyName, company.CompanyName);
            Assert.Equal(vatNumber, company.VatNumber);
            Assert.Equal(userId, company.Account?.UserId);
            Assert.Equal(userName, company.Account?.UserName);
        }
        #endregion

        #region ChangeCompanyName test
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ChangeCompanyName_Should_Throw_ArgumentException_If_Company_Name_Is_Empty(string companyName)
        {
            var company = Company.Register("company", "1234567890");

            var ex = Assert.Throws<ArgumentException>(() => company.ChangeCompanyName(companyName));
            Assert.Equal(nameof(companyName), ex.ParamName);
        }

        [Fact]
        public void ChangeCompanyName_Should_Change_Company_Name_With_Specified_Values()
        {
            var company = Company.Register("company", "1234567890");

            string companyName = "newcompany";
            company.ChangeCompanyName(companyName);

            Assert.Equal(companyName, company.CompanyName);
        }
        #endregion

        #region ChangeVatNumber test
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ChangeVatNumber_Should_Throw_ArgumentException_If_Vat_Number_Is_Empty(string vatNumber)
        {
            var company = Company.Register("company", "1234567890");

            var ex = Assert.Throws<ArgumentException>(() => company.ChangeVatNumber(vatNumber));
            Assert.Equal(nameof(vatNumber), ex.ParamName);
        }

        [Fact]
        public void ChangeVatNumber_Should_Change_The_Vat_Number_With_Specified_Value()
        {
            var company = Company.Register("company", "1234567890");

            string vatNumber = "0987654321";
            company.ChangeVatNumber(vatNumber);

            Assert.Equal(vatNumber, company.VatNumber);
        }
        #endregion
    }
}
