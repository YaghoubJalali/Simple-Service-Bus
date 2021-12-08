using Sample.ServiceBus.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.ServiceBus.Handler
{
    public class EventAggregator : IEventAggregator
    {
        private List<object> _subscribers = new List<object>();
        public void Publish<T>(T eventToPublish) where T : class, IEvent
        {
            var handler = _subscribers.Cast<IEventHandler<T>>().ToList();
            handler.ForEach(a => {
                a.Handle(eventToPublish);
            });
        }

        public void Subscribe<T>(IEventHandler<T> handler) where T : class,IEvent
        {
            _subscribers.Add(handler);
        }
    }
}
