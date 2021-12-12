using Sample.ServiceBus.Contract;
using Sample.ServiceBus.Contract.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.ServiceBus
{
    public class Event : IEvent
    {
        public Guid Id { get; private set; }
        public Event(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));

            Id = id;
        }
    }
}
