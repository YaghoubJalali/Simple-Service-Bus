using Sample.ServiceBus.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Command.UserCommand
{
    public class ModifyUserCommandMessage : CommandMessage
    {
        public ModifyUserCommandMessage(Guid id, string firstName, string lastName, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ValidateModifyUserCommandMessage();
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        private void ValidateModifyUserCommandMessage()
        {
            ValidateId();
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

        private void ValidateId()
        {
            if (this.Id.Equals(Guid.Empty))
                throw new ArgumentException("User guid is invalid!");
        }
    }
}
