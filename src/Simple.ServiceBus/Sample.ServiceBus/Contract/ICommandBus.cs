using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ServiceBus.Contract
{
    public interface ICommandBus
    {
        Task Dispatch<T>(T command) where T : class, ICommandMessage;
    }
}
