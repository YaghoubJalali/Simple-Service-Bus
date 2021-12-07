using Sample.ServiceBus.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.ServiceBus.Command
{
    public abstract class CommandMessage : ICommandMessage
    {
        public Guid Id { get; set; }
    }
}
