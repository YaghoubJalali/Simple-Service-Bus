using Sample.ServiceBus.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ServiceBus.Test.Handler.ExtraTemplateClassForTest
{
    public class TestEventHandler : IEventHandler<TestEvent>
    {
        public Task Handle(TestEvent eventToHandle)
        {
            throw new NotImplementedException();
        }
    }
}
