using Sample.UserManagement.Service.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sample.UserManagement.Service.Test.Model
{
    public class UserTest
    {
        [Fact]
        public void When_TryCreateUserWithInvalidGuid_Then_ArgumentExceptionShouldBeRaised()
        {
            var userGuid = Guid.Empty;
            Action act = (() => 
                 new User(userGuid, "fName", "lName", "email")
                 );

            Assert.Throws<ArgumentException>(act);

        }

        [Fact]
        public void When_CreatUserWithValidData_Then_ValidInstanceOfUserShouldBeReturned()
        {
            var firstName = "fName";
            var lastName = "lName";
            var email = "email";

            var user = new User(Guid.NewGuid(), firstName, lastName, email);
            Assert.NotNull(user);
            Assert.IsType<User>(user);

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
        public void When_TryCreateUserWithInvalidFirstName_Then_ArgumentExceptionShouldBeRaised(string firstName)
        {
            var userGuid = Guid.Empty;
            Action act = (() =>
                 new User(Guid.NewGuid(),firstName, "lName", "email")
                 );

            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [MemberData(nameof(InvalidStringPropertyValue))]
        public void When_TryCreateUserWithInvalidLastName_Then_ArgumentExceptionShouldBeRaised(string lastName)
        {
            var userGuid = Guid.Empty;
            Action act = (() =>
                 new User(Guid.NewGuid(), "fName", lastName, "email")
                 );

            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [MemberData(nameof(InvalidStringPropertyValue))]
        public void When_TryCreateUserWithInvalidEmail_Then_ArgumentExceptionShouldBeRaised(string email)
        {
            var userGuid = Guid.Empty;
            Action act = (() =>
                 new User(Guid.NewGuid(), "fName", "lName", email)
                 );

            Assert.Throws<ArgumentException>(act);
        }
    }
}
