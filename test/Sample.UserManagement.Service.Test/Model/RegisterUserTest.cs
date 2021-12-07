using Sample.UserManagement.Service.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sample.UserManagement.Service.Test.Model
{
    public class RegisterUserTest
    {
        [Fact]
        public void When_CreatRegisterUserWithValidData_Then_ValidInstanceShouldBeReturned()
        {
            var firstName = "fName";
            var lastName = "lName";
            var email = "email";

            var user = new RegisterUser(firstName, lastName, email);

            Assert.NotNull(user);
            Assert.IsType<RegisterUser>(user);

            Assert.True(user.FirstName.Equals(firstName));
            Assert.True(user.LastName.Equals(lastName));
            Assert.True(user.Email.Equals(email));

        }

        public static IEnumerable<Object[]> InvalidStringPropertyValue =>
            new List<object[]>
            {
                new object[] { null },
                new object[] { string.Empty },
                new object[] { "   " },
            };

        [Theory]
        [MemberData(nameof(InvalidStringPropertyValue))]
        public void When_TryCreateRegisterUserWithInvalidFirstName_Then_ArgumentExceptionShouldBeRaised(string firstName)
        {
            var userGuid = Guid.Empty;
            Action act = (() =>
                 new RegisterUser(firstName, "lName", "email")
                 );

            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [MemberData(nameof(InvalidStringPropertyValue))]
        public void When_TryCreateRegisterUserWithInvalidLastName_Then_ArgumentExceptionShouldBeRaised(string lastName)
        {
            var userGuid = Guid.Empty;
            Action act = (() =>
                 new RegisterUser("fName", lastName, "email")
                 );

            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [MemberData(nameof(InvalidStringPropertyValue))]
        public void When_TryCreateRegisterUserWithInvalidEmail_Then_ArgumentExceptionShouldBeRaised(string email)
        {
            var userGuid = Guid.Empty;
            Action act = (() =>
                 new RegisterUser("fName", "lName", email)
                 );

            Assert.Throws<ArgumentException>(act);
        }
    }
}
