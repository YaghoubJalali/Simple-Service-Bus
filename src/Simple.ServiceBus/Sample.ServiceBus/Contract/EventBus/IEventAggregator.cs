using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.ServiceBus.Contract
{
    interface IEventAggregator
    {
        void Publish<T>(T eventToPublish) where T : class,IEvent;
        void Subscribe<T>(IEventHandler<T> handler) where T :class,IEvent;
    }
}
