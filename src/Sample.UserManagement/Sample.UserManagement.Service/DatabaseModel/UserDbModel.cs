using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.DatabaseModel
{
    public class UserDbModel:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public override bool Equals(object obj)
        {
            var objUserModel = obj as UserDbModel;

            if (objUserModel == null)
            {
                return false;
            }

            return (this.Id.Equals(objUserModel.Id)
                && this.FirstName == objUserModel.FirstName
                && this.LastName == objUserModel.LastName
                && this.Email == objUserModel.Email
                );
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, Email);
        }
    }
}
