using Sample.Framework.Common.ServiceProvider;
using Sample.ServiceBus.Contract;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ServiceBus.Handler
{
    public class CommandBus : ICommandBus 
    {
        private readonly IServicesProvider _provier;

        public CommandBus(IServicesProvider provider)
        {
            _provier = provider ?? throw new ArgumentNullException($"{nameof(IServicesProvider)} is null!");
        }

        public async Task Dispatch<T>(T command) where T : class, ICommandMessage
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var handler = _provier.GetService<ICommandHandler<T>>();
            if (handler == null)
                throw new ArgumentNullException(nameof(ICommandHandler<T>));

            await handler.HandelAsync(command);
        }
    }
}
