using Sample.ServiceBus.Contract.EventBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ServiceBus.Contract
{
    //public interface IEventHandler { }
    public interface IEventHandler<TEvent> 
                  where TEvent: IEvent
    {
        Task HandleAsync(TEvent eventToHandle);
    }
}
