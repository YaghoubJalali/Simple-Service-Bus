﻿using Sample.ServiceBus.Handler;
using Sample.UserManagement.Service.Event;
using Sample.UserManagement.Service.Event.Handler;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Sample.ServiceBus.Contract;
using Sample.ServiceBus.Contract.EventBus;
using System.Threading.Tasks;
using Sample.ServiceBus.Test.Handler.ExtraTemplateClassForTest;
using Moq;

namespace Sample.ServiceBus.Test.Handler
{
    public class EventAggregatorTest : IClassFixture<EventAggregatorFixture>
    {
        private readonly EventAggregatorFixture _eventAggregatorFixture;

        public EventAggregatorTest(EventAggregatorFixture eventAggregatorFixture)
        {
            _eventAggregatorFixture = eventAggregatorFixture;
        }

        [Fact]
        public void When_TryToCreateCommandBusWithNullParameter_Then_ArgumentNullExceptionShouldBeThrown()
        {
            Action act = (() => new EventAggregator(null));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void When_SubscribeEventHandlerInEventAggregator_Then_TypeOfSubscriberShouldBeInsertInSubscribersTypeList()
        {
            var eventAggregator = new EventAggregator(_eventAggregatorFixture.ServiceProvider);
            eventAggregator.Subscribe<UserCreatedEventHandler, UserCreatedEvent>();
            Assert.True(eventAggregator.SubscriberTypes.ToList().Count()>0);
            
            var subscriber = eventAggregator.SubscriberTypes.FirstOrDefault(o => o == typeof(IEventHandler<UserCreatedEvent>));
            Assert.NotNull(subscriber);
        }

        [Fact]
        public void When_TryToPublishNullEvent_Then_ArgumentNullExceptionShouldBeThrown()
        {
            IEvent eventToPublish = null;
            async Task act() => await _eventAggregatorFixture.EventAggregator.Publish(eventToPublish);

            Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public void When_TryToPublishEventThatHandlerDoesNotRegistered_Then_ArgumentNullExceptionShouldBeThrown()
        {
            var eventAggregator = new EventAggregator(_eventAggregatorFixture.ServiceProvider);
            eventAggregator.Subscribe<InvalidTestEventHandler, InvalidEventForTest>();

            var eventToPublish = new InvalidEventForTest(Guid.NewGuid());

            async Task act() => await eventAggregator.Publish(eventToPublish);

            Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task When_PublishUserCreatedEventThen_Then_WellcomEmailShouldBeSendForUser()
        {
            var userGuid = Guid.NewGuid();
            var userCreatedEvent = new UserCreatedEvent(userGuid);
            await _eventAggregatorFixture.EventAggregator.Publish(userCreatedEvent);
            
            _eventAggregatorFixture.EmailService.Verify(m =>
                        m.SendWelcomeMailTo(userGuid)
                        , Moq.Times.Once);
        }
    }
}