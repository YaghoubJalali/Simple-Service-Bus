using Sample.ServiceBus.Contract.QueryBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Model
{
    public class GetUsersQueryResult : IResult
    {
        public List<User> Users { get; set; }
    }
}
