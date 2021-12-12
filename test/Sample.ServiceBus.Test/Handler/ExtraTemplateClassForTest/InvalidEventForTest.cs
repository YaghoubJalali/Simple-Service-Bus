using Sample.ServiceBus.Contract.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.ServiceBus.Test.Handler.ExtraTemplateClassForTest
{
    public class InvalidEventForTest : ServiceBus.Event
    {
        public InvalidEventForTest(Guid id) : base(id)
        {
        }
    }
}
