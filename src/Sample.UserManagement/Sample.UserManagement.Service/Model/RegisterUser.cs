using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Model
{
   public  class RegisterUser : BaseUser
    {
        public RegisterUser(string firstName, string lastName, string email)
            :base(firstName,lastName,email)
        {
        }
    }
}
