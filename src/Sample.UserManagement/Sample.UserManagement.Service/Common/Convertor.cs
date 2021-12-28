using Sample.UserManagement.Service.DatabaseModel;
using Sample.UserManagement.Service.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Common
{
    public static class Convertor
    {
        public static User ConvertUserDbModelToUser(UserDbModel model)
        {
            if (model == null)
                return null;

            return new User(model.Id, model.FirstName, model.LastName, model.Email);
        }
    }
}
