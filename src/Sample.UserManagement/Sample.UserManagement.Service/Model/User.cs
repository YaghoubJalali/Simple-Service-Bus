using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Model
{
    public class User
    {
        public Guid UserGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public User(Guid userGuid, string firstName, string lastName, string email)
        {
            UserGuid = userGuid;
            FirstName = firstName;
            LastName = lastName;
            Email = email;

            ValidateModel();
        }

        private void ValidateModel()
        {
            ValidateUserGuid();
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

        private void ValidateUserGuid()
        {
            if (this.UserGuid.Equals(Guid.Empty))
                throw new ArgumentException("User guid is invalid!");
        }
    }
}
