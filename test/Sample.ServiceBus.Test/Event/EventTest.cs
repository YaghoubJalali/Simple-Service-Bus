using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sample.ServiceBus.Test.Event
{
    public class EventTest
    {
        [Fact]
        public void When_CreateEventClassWithEmptyGUIDParameter_Then_ArgumentExceptionShouldBeThrow()
        {
            Assert.Throws<ArgumentException>(() => new ServiceBus.Event(Guid.Empty));
        }

        [Fact]
        public void When_CreateEventClassWithGuidValidParameter_Then_GuidParamterShouldBeSetInIdProperty()
        {
            var id = Guid.NewGuid();
            var eventInstance = new ServiceBus.Event(id);
            Assert.NotNull(eventInstance);
            Assert.Equal(id, eventInstance.Id);
        }
    }
}
