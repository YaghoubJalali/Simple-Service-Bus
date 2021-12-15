using Sample.Framework.Common.ServiceProvider;
using Sample.ServiceBus.Contract;
using Sample.ServiceBus.Contract.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ServiceBus.Handler
{
    public class EventAggregator : IEventAggregator
    {
        private readonly IServicesProvider _provier;
        private static List<Type> _subscribersType = new List<Type>();
        public static IEnumerable<Type> SubscriberTypes
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

        public void SubscribeEventHandler<T, U>()
            where T : IEventHandler<U>
            where U : IEvent
        {
            _subscribersType.Add(typeof(IEventHandler<U>));
        }

        public void SubscribeActionHandler<T>() where T : IEvent
        {
            _subscribersType.Add(typeof(ActionEventHandler<T>));
        }

        public async Task PublishAsync<T>(T eventToPublish) where T : IEvent
        {
            if(eventToPublish == null)
                throw new ArgumentNullException(nameof(eventToPublish));

            var handlers = GetEventSubscribers(eventToPublish);
            List<Task> actionToHandle = new List<Task>();
            foreach (var handler in handlers)
            {
                var handlerService = GetService<T>(handler);
                if (handlerService == null)
                    throw new ArgumentNullException($"{typeof(IEventHandler<T>)}");

                actionToHandle.Add(handlerService.HandleAsync(eventToPublish));
            }

            await Task.WhenAll(actionToHandle);
        }

        private List<Type> GetEventSubscribers<T>(T eventToPublish) where T : IEvent
        {
            List<Type> types = new List<Type>();
            var handlerTypes = _subscribersType.Where(o => o == typeof(IEventHandler<T>)).ToList();
            types.AddRange(handlerTypes);

            var actionHandlerTypes = _subscribersType.Where(o => o == typeof(ActionEventHandler<T>)).ToList();
            types.AddRange(actionHandlerTypes);

            return types;
        }

        private IEventHandler<T> GetService<T>(Type type) where T : IEvent
        {
            MethodInfo method = _provier.GetType().GetMethod("GetService");
            var genericMethod = method.MakeGenericMethod(type);
            var service = genericMethod.Invoke(_provier, null);
            return service as IEventHandler<T>;
        }
    }
}
