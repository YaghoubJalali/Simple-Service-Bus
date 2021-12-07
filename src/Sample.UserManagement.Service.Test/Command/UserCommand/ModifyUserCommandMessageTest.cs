using Sample.UserManagement.Service.Command.UserCommand;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sample.UserManagement.Service.Test.Command.UserCommand
{
    public class ModifyUserCommandMessageTest
    {
        [Fact]
        public void When_TryCreateModifyUserCommandMessageWithInvalidGuid_Then_ArgumentExceptionShouldBeRaised()
        {
            var userGuid = Guid.Empty;
            Action act = (() =>
                 new ModifyUserCommandMessage(userGuid, "fName", "lName", "email")
                 );

            Assert.Throws<ArgumentException>(act);

        }

        [Fact]
        public void When_CreatModifyUserCommandMessageWithValidData_Then_ValidInstanceShouldBeReturned()
        {
            var id = Guid.NewGuid();
            var firstName = "fName";
            var lastName = "lName";
            var email = "email";

            var commandMessage = new ModifyUserCommandMessage(id,firstName, lastName, email);
            Assert.NotNull(commandMessage);
            Assert.IsType<ModifyUserCommandMessage>(commandMessage);

            Assert.True(commandMessage.FirstName.Equals(firstName));
            Assert.True(commandMessage.LastName.Equals(lastName));
            Assert.True(commandMessage.Email.Equals(email));
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
        public void When_TryCreateModifyCommandMessageWithInvalidFirstName_Then_ArgumentExceptionShouldBeRaised(string firstName)
        {
            Action act = (() =>
                 new ModifyUserCommandMessage(Guid.NewGuid(),firstName, "lName", "email")
                 );

            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [MemberData(nameof(InvalidStringPropertyValue))]
        public void When_TryCreateModifyUserCommandMessageWithInvalidLastName_Then_ArgumentExceptionShouldBeRaised(string lastName)
        {
            Action act = (() =>
                 new ModifyUserCommandMessage(Guid.NewGuid(),"fName", lastName, "email")
                 );

            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [MemberData(nameof(InvalidStringPropertyValue))]
        public void When_TryCreateModifyUserCommandMessageWithInvalidEmail_Then_ArgumentExceptionShouldBeRaised(string email)
        {
            Action act = (() =>
                 new ModifyUserCommandMessage(Guid.NewGuid(), "fName", "lName", email)
                 );

            Assert.Throws<ArgumentException>(act);
        }
    }
}
