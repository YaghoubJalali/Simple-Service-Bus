using Sample.UserManagement.Service.Command.UserCommand;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sample.UserManagement.Service.Test.Command.UserCommand
{
   public class RegisterUserCommandMessageTest
    {
        [Fact]
        public void When_CreatRegisterUserCommandMessageWithValidData_Then_ValidInstanceShouldBeReturned()
        {
            var firstName = "fName";
            var lastName = "lName";
            var email = "email";

            var commandMessage = new RegisterUserCommandMessage(firstName, lastName, email);
            Assert.NotNull(commandMessage);
            Assert.IsType<RegisterUserCommandMessage>(commandMessage);

            Assert.True(commandMessage.FirstName.Equals(firstName));
            Assert.True(commandMessage.LastName.Equals(lastName));
            Assert.True(commandMessage.Email.Equals(email));
            Assert.NotEqual(Guid.Empty, commandMessage.Id);
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
        public void When_TryCreateRegisterUserCommandMessageWithInvalidFirstName_Then_ArgumentExceptionShouldBeRaised(string firstName)
        {
            Action act = (() =>
                 new RegisterUserCommandMessage(firstName, "lName", "email")
                 );

            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [MemberData(nameof(InvalidStringPropertyValue))]
        public void When_TryCreateRegisterUserCommandMessageWithInvalidLastName_Then_ArgumentExceptionShouldBeRaised(string lastName)
        {
            Action act = (() =>
                 new RegisterUserCommandMessage("fName", lastName, "email")
                 );

            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [MemberData(nameof(InvalidStringPropertyValue))]
        public void When_TryCreateRegisterUserCommandMessageWithInvalidEmail_Then_ArgumentExceptionShouldBeRaised(string email)
        {
            Action act = (() =>
                 new RegisterUserCommandMessage("fName", "lName", email)
                 );

            Assert.Throws<ArgumentException>(act);
        }
    }
}
