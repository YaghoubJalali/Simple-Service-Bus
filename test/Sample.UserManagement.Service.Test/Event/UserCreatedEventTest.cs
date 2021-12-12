using Sample.UserManagement.Service.Event;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sample.UserManagement.Service.Test.Event
{
    public class UserCreatedEventTest
    {
        [Fact]
        public void When_CreateUserCreatedEventClassWithEmptyGUIDParameter_Then_ArgumentExceptionShouldBeThrow()
        {
            Assert.Throws<ArgumentException>(() => new UserCreatedEvent(Guid.Empty));
        }

        [Fact]
        public void When_CreateUserCreatedEventClassWithGuidValidParameter_Then_GuidParamterShouldBeSetInIdProperty()
        {
            var id = Guid.NewGuid();
            var eventInstance = new UserCreatedEvent(id);
            Assert.NotNull(eventInstance);
            Assert.Equal(id, eventInstance.Id);
        }

    }
}
