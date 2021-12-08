using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.ServiceBus.Contract
{
    //public interface IEventHandler { }
    public interface IEventHandler<in TEvent> 
                  where TEvent:class, IEvent
    {
        void Handle(TEvent eventToHandle);
    }
}
