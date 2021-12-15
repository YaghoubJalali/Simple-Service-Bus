using Sample.UserManagement.Service.Event;
using Sample.UserManagement.Service.Event.Handler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sample.UserManagement.Service.Test.Event.Handler
{
    public class UserCreatedEventHandlerTest:IClassFixture<UserCreatedEventHandlerFixture>
    {
        private readonly UserCreatedEventHandlerFixture  _userCreatedEventHandlerFixture;

        public UserCreatedEventHandlerTest(UserCreatedEventHandlerFixture  userCreatedEventHandlerFixture)
        {
            this._userCreatedEventHandlerFixture = userCreatedEventHandlerFixture;
        }

        [Fact]
        public void When_TryToInstanciateUserCreatedEventHandlerWithInvalidParameter_Then_ArgumantNullExceptionSHouldBeThrown()
        {
            Assert.Throws<ArgumentNullException>(() => { new UserCreatedEventHandler(null); });
        }

        [Fact]
        public async Task When_CallHandleMethodWithNullEvent_Then_ArgumentNullExceptionShouldBeThrown()
        {
            async Task act() => await _userCreatedEventHandlerFixture.MockUserCreatedEventHandler.HandleAsync(null);
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task When_CallHandleMethodWithValidEvent_Then_SendWelcomeMailToMethodInMailServiceShouldBeCalled()
        {
            var eventToHandle = new UserCreatedEvent(Guid.NewGuid());

            await _userCreatedEventHandlerFixture.MockUserCreatedEventHandler.HandleAsync(eventToHandle);
            _userCreatedEventHandlerFixture.MockEmailService.Verify(m => m.SendWelcomeMailTo(eventToHandle.Id), Moq.Times.Once);

        }
    }
}
