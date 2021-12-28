using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ServiceBus.Contract
{
    public interface ICommandBus
    {
        Task DispatchAsync<T>(T command) where T : class, ICommandMessage;
    }
}
