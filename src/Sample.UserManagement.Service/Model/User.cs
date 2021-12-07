using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Model
{
    public class User
    {
        public Guid UserGuid { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public User(string firstName, string lastName)
        {
            UserGuid = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;

            ValidateUser();
        }

        private void ValidateUser()
        {
            if (string.IsNullOrWhiteSpace(FirstName)
                || string.IsNullOrWhiteSpace(LastName))
                throw new Exception("Invalid FirstName or LastName!");
        }

    }
}
