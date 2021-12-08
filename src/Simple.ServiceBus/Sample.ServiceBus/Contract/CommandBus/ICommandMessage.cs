using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.ServiceBus.Contract
{
    public interface ICommandMessage
    {
        Guid Id { get; }
    }
}
