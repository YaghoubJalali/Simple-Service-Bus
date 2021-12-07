using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ServiceBus.Contract
{
    public interface ICommandHandler { }
    public interface ICommandHandler<in TCommand>
        : ICommandHandler where TCommand : class, ICommandMessage
    {
        Task HandelAsync(TCommand command);
    }
}
