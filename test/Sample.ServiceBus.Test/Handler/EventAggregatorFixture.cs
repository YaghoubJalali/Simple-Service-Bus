using Moq;
using Sample.Framework.Common.ServiceProvider;
using Sample.ServiceBus.Contract;
using Sample.ServiceBus.Handler;
using Sample.UserManagement.Service.Event;
using Sample.UserManagement.Service.Event.Handler;
using Sample.UserManagement.Service.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace Sample.ServiceBus.Test.Handler
{
    public class EventAggregatorFixture
    {
        public IEventAggregator EventAggregator { get; private set; }
        public IServicesProvider ServiceProvider { get; private set; }
        public Mock<UserCreatedEventHandler> UserCreatedEventHandler { get; private set; }
        public Mock<IEmailService> EmailService { get; private set; }
        public EventAggregatorFixture()
        {
            InitiateServices();
            RegisterEventHandlers();
        }

        private void InitiateServices()
        {
            var mockServiceProvider = new Mock<IServiceProvider>();
            ServiceProvider = new ServicesProvider(mockServiceProvider.Object);
            EmailService = new Mock<IEmailService>();
            UserCreatedEventHandler = new Mock<UserCreatedEventHandler>(EmailService.Object);

            mockServiceProvider.Setup(o => o.GetService(typeof(IEventHandler<UserCreatedEvent>)))
                .Returns(() =>
                {
                    return UserCreatedEventHandler.Object;
                });

            EventAggregator = new EventAggregator(ServiceProvider);
        }

        private void RegisterEventHandlers()
        {
            EventAggregator.SubscribeEventHandler<UserCreatedEventHandler, UserCreatedEvent>();
        }

    }
}
