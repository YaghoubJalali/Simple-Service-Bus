using Sample.ServiceBus.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ServiceBus.Test.Handler.ExtraTemplateClassForTest
{
    public class InvalidTestEventHandler : IEventHandler<InvalidEventForTest>
    {
        public Task Handle(InvalidEventForTest eventToHandle)
        {
            throw new NotImplementedException();
        }
    }
}
