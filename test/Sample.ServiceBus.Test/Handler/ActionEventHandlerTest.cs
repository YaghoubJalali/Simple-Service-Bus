using Moq;
using Sample.ServiceBus.Contract.EventBus;
using Sample.ServiceBus.Handler;
using Sample.ServiceBus.Test.Handler.ExtraTemplateClassForTest;
using Sample.UserManagement.Service.Event;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sample.ServiceBus.Test.Handler
{
    public class ActionEventHandlerTest : IClassFixture<ActionEventHandlerFixture>
    {
        private readonly ActionEventHandlerFixture  _actionEventHandlerFixture;
        public ActionEventHandlerTest(ActionEventHandlerFixture actionEventHandlerFixture)
        {
            _actionEventHandlerFixture = actionEventHandlerFixture;
        }

        [Fact]
        public void When_TryToInstanciateActionEventHandlerClassWithNullParameter_Then_ArgumentNullExceptionShouldBeThrown()
        {
            Func<TestEvent, Task> parameter = null;
            Action act = (
                () => new ActionEventHandler<TestEvent>(parameter)
               );
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public async Task When_InstanciateActionEventHandlerWithAnActionAndPublishEvent_Then_ActionShouldBeCalled()
        {
            bool actionMethodCalled = false;

            var userGuid = Guid.NewGuid();
            var testEvent = new TestEvent(userGuid);
            
            Task func(TestEvent func)
            {
                //Call any async method here
                actionMethodCalled = true;
                return Task.Delay(1);
            }

            var actionEventHandler = new ActionEventHandler<TestEvent>(func);
            _actionEventHandlerFixture.MockServiceProvider.Setup(o => o.GetService<ActionEventHandler<TestEvent>>())
                .Returns(() =>
                {
                    return actionEventHandler;
                });

            _actionEventHandlerFixture.EventAggregator.SubscribeActionHandler<TestEvent>();
            await _actionEventHandlerFixture.EventAggregator.PublishAsync(testEvent);

            Assert.True(actionMethodCalled);
        }
    }
}
