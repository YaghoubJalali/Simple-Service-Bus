using Sample.ServiceBus.Contract;
using Sample.ServiceBus.Contract.EventBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ServiceBus.Handler
{
    /// <summary>
    /// Add action to specific event and handle it 
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public class ActionEventHandler<TEvent> : IEventHandler<TEvent>
                  where TEvent : IEvent
    {
        private Func<TEvent,Task> _actionEvent { get; }
        public ActionEventHandler(Func<TEvent, Task> actionEvent)
        {
            _actionEvent = actionEvent?? throw new ArgumentNullException(nameof(actionEvent));
        }

        public async Task Handle(TEvent eventToHandle)
        {
           await  _actionEvent(eventToHandle);
        }
    }
}
