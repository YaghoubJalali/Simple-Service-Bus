using Sample.ServiceBus.Contract.EventBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ServiceBus.Contract
{
    public interface IEventAggregator
    {
        Task Publish<TEvent>(TEvent eventToPublish) where TEvent : IEvent;

        //UserCreatedEventHandler : IEventHandler<UserCreatedEvent>

        void Subscribe<T,U>() 
            where T : IEventHandler<U>
            where U : IEvent;

    }
}
