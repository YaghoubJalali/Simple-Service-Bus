using Sample.UserManagement.Service.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sample.UserManagement.Service.Test.Model
{
    public class ModifyUserTest
    {
        [Fact]
        public void When_CreatModifyUserWithValidData_Then_ValidInstanceShouldBeReturned()
        {
            var firstName = "fName";
            var lastName = "lName";
            var email = "email";

            var user = new ModifyUser(firstName, lastName, email);

            Assert.NotNull(user);
            Assert.IsType<ModifyUser>(user);

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
        public void When_TryCreateModifyUserWithInvalidFirstName_Then_ArgumentExceptionShouldBeRaised(string firstName)
        {
            var userGuid = Guid.Empty;
            Action act = (() =>
                 new ModifyUser(firstName, "lName", "email")
                 );

            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [MemberData(nameof(InvalidStringPropertyValue))]
        public void When_TryCreateModifyUserWithInvalidLastName_Then_ArgumentExceptionShouldBeRaised(string lastName)
        {
            var userGuid = Guid.Empty;
            Action act = (() =>
                 new ModifyUser("fName", lastName, "email")
                 );

            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [MemberData(nameof(InvalidStringPropertyValue))]
        public void When_TryCreateModifyUserWithInvalidEmail_Then_ArgumentExceptionShouldBeRaised(string email)
        {
            var userGuid = Guid.Empty;
            Action act = (() =>
                 new ModifyUser("fName", "lName", email)
                 );

            Assert.Throws<ArgumentException>(act);
        }
    }
}
