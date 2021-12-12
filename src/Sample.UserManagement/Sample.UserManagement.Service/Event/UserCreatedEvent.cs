using Sample.ServiceBus.Contract;
using Sample.ServiceBus.Contract.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Event
{
    public class UserCreatedEvent : ServiceBus.Event
    {
        public UserCreatedEvent(Guid id) : base(id)
        {
        }
    }
}
