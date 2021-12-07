using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Model
{
    public abstract class BaseUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public BaseUser(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ValidateModel();
        }

        private void ValidateModel()
        {
            ValidateFirstName();
            ValidateLastName();
            ValidateEmail();
        }

        public virtual void ValidateFirstName()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
                throw new ArgumentException("Invalid FirstName!");
        }
        public virtual void ValidateLastName()
        {
            if (string.IsNullOrWhiteSpace(LastName))
                throw new ArgumentException("Invalid LastName!");
        }
        public virtual void ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
                throw new ArgumentException("Invalid Email!");
        }
    }
}
