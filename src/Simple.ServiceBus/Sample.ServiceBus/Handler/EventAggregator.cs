using Sample.Framework.Common.ServiceProvider;
using Sample.ServiceBus.Contract;
using Sample.ServiceBus.Contract.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ServiceBus.Handler
{
    public class EventAggregator : IEventAggregator
    {
        private readonly IServicesProvider _provier;
        private readonly List<Type> _subscribersType = new List<Type>();
        public IEnumerable<Type> SubscriberTypes
        {
            get
            {
                return _subscribersType.AsReadOnly();
            }
        }

        public EventAggregator(IServicesProvider provider)
        {
            _provier = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public void Subscribe<T, U>()
            where T : IEventHandler<U>
            where U : IEvent
        {
            _subscribersType.Add(typeof(IEventHandler<U>));
        }

        public async Task Publish<T>(T eventToPublish) where T : IEvent
        {
            if(eventToPublish == null)
                throw new ArgumentNullException(nameof(eventToPublish));

            var handlers = GetEventSubscribers(eventToPublish);
            List<Task> actionToHandle = new List<Task>();
            foreach (var handler in handlers)
            {
                var handlerService = _provier.GetService<IEventHandler<T>>();
                if (handlerService == null)
                    throw new ArgumentNullException($"{typeof(IEventHandler<T>)}");

                actionToHandle.Add(handlerService.Handle(eventToPublish));
            }

            await Task.WhenAll(actionToHandle);
        }

        private List<Type> GetEventSubscribers<T>(T eventToPublish) where T : IEvent
        {
            return _subscribersType.Where(o => o == typeof(IEventHandler<T>)).ToList();
        }
    }
}
