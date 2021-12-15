using Sample.ServiceBus.Contract.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.ServiceBus.Test.Handler.ExtraTemplateClassForTest
{
    public class TestEvent : ServiceBus.Event
    {
        public TestEvent(Guid id) : base(id)
        {
        }
    }
}
