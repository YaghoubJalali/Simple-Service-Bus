﻿using Sample.ServiceBus.Contract.QueryBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Model
{
    public class GetUserQuery : IQuery
    {
        public Guid UserId { get; set; }
    }

    

}
