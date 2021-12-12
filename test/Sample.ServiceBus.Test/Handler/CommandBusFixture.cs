using Sample.Framework.Common.ServiceProvider;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Sample.UserManagement.Service.Command.UserCommand;
using Sample.ServiceBus.Contract;
using Sample.ServiceBus.Handler;
using Sample.UserManagement.Service.Repository;
using Sample.UserManagement.Service.Repository.Contract;

namespace Sample.ServiceBus.Test.Handler
{
    public class CommandBusFixture
    {
        public ICommandBus CommandBus { get; private set; }
        public Mock<UserCommandHandler> MockUserCommandHandler { get; private set; }
        public Mock<IUserRepository>  MockUserRepository { get; private set; }
        public Mock<IEventAggregator> MockEventAggregator { get; private set; }


        public IServicesProvider ServiceProvider { get; private set; }

        public CommandBusFixture()
        {
            InitiateServices();
        }

        private void InitiateServices()
        {
            MockUserRepository = new Mock<IUserRepository>();
            MockEventAggregator = new Mock<IEventAggregator>();
            MockUserCommandHandler = new Mock<UserCommandHandler>
                (MockUserRepository.Object, MockEventAggregator.Object);

            var mockServiceProvider = new Mock<IServiceProvider>();
            ServiceProvider = new ServicesProvider(mockServiceProvider.Object);
            mockServiceProvider.Setup(o => o.GetService(typeof(ICommandHandler<NullCommandMessageForTest>)))
                .Returns(() =>
                {
                    return null;
                });

            mockServiceProvider.Setup(o => o.GetService(typeof(ICommandHandler<RegisterUserCommandMessage>)))
                .Returns(() =>
                {
                    return MockUserCommandHandler.Object;
                });

            mockServiceProvider.Setup(o => o.GetService(typeof(ICommandHandler<ModifyUserCommandMessage>)))
               .Returns(() =>
               {
                   return MockUserCommandHandler.Object;
               });

            CommandBus = new CommandBus(ServiceProvider);
        }
    }
}
