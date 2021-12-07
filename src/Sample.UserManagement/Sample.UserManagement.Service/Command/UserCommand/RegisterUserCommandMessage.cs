using Sample.ServiceBus.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Command.UserCommand
{
    public class RegisterUserCommandMessage: CommandMessage
    {
        public RegisterUserCommandMessage(string firstName, string lastName,string email)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;

            ValidateUserCommandMessage();

        }
        public string FirstName { get;private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        private void ValidateUserCommandMessage()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateEmail();
        }

        private void ValidateFirstName()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
                throw new ArgumentException("FirstName is invlid!");
        }
        private void ValidateLastName()
        {
            if (string.IsNullOrWhiteSpace(LastName))
                throw new ArgumentException("LastName is invlid!!");
        }
        private void ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
                throw new ArgumentException("Email is invlid!");
        }
    }
}
